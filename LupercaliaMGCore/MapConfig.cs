using System.Globalization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class MapConfig {
        private LupercaliaMGCore m_CSSPlugin;

        // Config name and path
        private List<MapConfigFile> configs = new List<MapConfigFile>();

        private string configFolder;

        public MapConfig(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
            
            configFolder = Path.GetFullPath(Path.Combine(Server.GameDirectory, PluginSettings.CONFIG_FOLDER, "map/"));

            if(!checkDirectoryExists())
                throw new InvalidOperationException("Map config directory is not exists and failed to create.");

            updateConfigsDictionary();

            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart, HookMode.Post);
            m_CSSPlugin.RegisterListener<Listeners.OnMapStart>(OnMapStart);
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {            
            if(!MathUtil.DecomposePowersOfTwo(PluginSettings.getInstance.m_CVMapConfigExecutionTiming.Value).Contains(2))
                return HookResult.Continue;

            executeConfigs();
            return HookResult.Continue;
        }

        private void OnMapStart(string mapName) {
            if(!MathUtil.DecomposePowersOfTwo(PluginSettings.getInstance.m_CVMapConfigExecutionTiming.Value).Contains(1))
                return;
            
            executeConfigs();
        }

        private void executeConfigs() {
            m_CSSPlugin.Logger.LogDebug("Updating the config dictionary");
            updateConfigsDictionary();

            int mapCfgType = PluginSettings.getInstance.m_CVMapConfigType.Value;

            m_CSSPlugin.Logger.LogDebug("Checking the Map Config type");
            if(mapCfgType == 0)
                return;

            m_CSSPlugin.Logger.LogDebug("Iterating the config file");

            string? mapName = Server.MapName;

            if(mapName == null)
                return;

            foreach(MapConfigFile conf in configs) {
                m_CSSPlugin.Logger.LogDebug("Checking the map config type");

                if(MathUtil.DecomposePowersOfTwo(mapCfgType).Contains(2) && !mapName.StartsWith(conf.name))
                    continue;
                
                
                m_CSSPlugin.Logger.LogDebug("Checking the map config type 2");
                
                m_CSSPlugin.Logger.LogTrace($"{conf.name} {conf.path}");

                if(MathUtil.DecomposePowersOfTwo(mapCfgType).Contains(1) && !mapName.Equals(conf.name))
                    continue;

                m_CSSPlugin.Logger.LogDebug("Executing config!");
                Server.ExecuteCommand($"exec {conf.path}");
            }
        }

        private void updateConfigsDictionary() {
            m_CSSPlugin.Logger.LogDebug("Get files from directory");
            string[] files = Directory.GetFiles(configFolder, "", SearchOption.TopDirectoryOnly);

            m_CSSPlugin.Logger.LogDebug("Clearing configs");
            configs.Clear();
            foreach(string file in files) {
                string fileName = Path.GetFileName(file);
                if(!fileName.EndsWith(".cfg", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                string relativePath = Path.GetRelativePath(Path.GetFullPath(Path.Combine(Server.GameDirectory, "csgo/cfg/")), file);

                m_CSSPlugin.Logger.LogTrace("Adding config");
                configs.Add(new MapConfigFile(fileName[..fileName.LastIndexOf(".")], relativePath));
            }
        }

        private bool checkDirectoryExists() {
            if(!Directory.Exists(configFolder)) {
                try {
                    m_CSSPlugin.Logger.LogWarning($"Map config folder {configFolder} is not exists. Trying to create...");

                    Directory.CreateDirectory(configFolder);
                } catch(Exception e) {
                    m_CSSPlugin.Logger.LogError($"Failed to create map config folder!");
                    m_CSSPlugin.Logger.LogError(e.StackTrace);
                    return false;
                }
            }

            return true;
        }
    }
}