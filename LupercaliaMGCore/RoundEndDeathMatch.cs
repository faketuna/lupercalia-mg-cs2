using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;

namespace LupercaliaMGCore {
    public class RoundEndDeathMatch {
        private LupercaliaMGCore m_CSSPlugin;

        private ConVar? mp_teammates_are_enemies = null;

        public RoundEndDeathMatch(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            trySetValue("0");

            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart);
            m_CSSPlugin.RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {
            SimpleLogging.LogDebug("[Round End Death Match] Called RoundPreStart.");
            if(!PluginSettings.getInstance.m_CVIsRoundEndDeathMatchEnabled.Value) {
                SimpleLogging.LogDebug("[Round End Death Match] REDM is disabled doing nothing.");
                return HookResult.Continue;
            }
            trySetValue("0");
            SimpleLogging.LogDebug("[Round End Death Match] Ended.");
            return HookResult.Continue;
        }

        private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info) {
            SimpleLogging.LogDebug("[Round End Death Match] Called RoundEnd");
            if(!PluginSettings.getInstance.m_CVIsRoundEndDeathMatchEnabled.Value) {
                SimpleLogging.LogDebug("[Round End Death Match] REDM is disabled doing nothing.");
                return HookResult.Continue;
            }
            trySetValue("1");
            SimpleLogging.LogDebug("[Round End Death Match] Started.");
            return HookResult.Continue;
        }

        private void trySetValue(string value) {
            if(mp_teammates_are_enemies == null) {
                mp_teammates_are_enemies = ConVar.Find("mp_teammates_are_enemies");
            }

            mp_teammates_are_enemies?.SetValue(value);
        }
    }
}