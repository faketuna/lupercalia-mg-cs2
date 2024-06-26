using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using System.Drawing;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Timers;

namespace LupercaliaMGCore {
    public class VoteMapRestart
    {
        private LupercaliaMGCore m_CSSPlugin;

        private List<CCSPlayerController> votedPlayers = new List<CCSPlayerController>();
        private int playersRequiredToRestart = 0;
        private double mapStartTime = 0.0D;
        private bool isMapRestarting = false;

        public VoteMapRestart(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            mapStartTime = Server.EngineTime;
            m_CSSPlugin.RegisterEventHandler<EventMapTransition>(OnMapTransition);
            m_CSSPlugin.RegisterEventHandler<EventGameNewmap>(OnMapFullyLoaded);
            m_CSSPlugin.AddCommand("css_vmr", "Vote map restart command", CommandVoteRestartMap);
        }

        private void CommandVoteRestartMap(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;

            SimpleLogging.LogDebug($"[Vote Map Restart] [Player {client.PlayerName}] trying to vote for restart map.");
            if(isMapRestarting) {
                SimpleLogging.LogDebug($"[Vote Map Restart] [Player {client.PlayerName}] map is already restarting in progress.");
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Map is already restarting in progress!"));
                return;
            }

            if(Server.EngineTime - mapStartTime  > PluginSettings.getInstance.m_CVVoteMapRestartAllowedTime.Value) {
                SimpleLogging.LogDebug($"[Vote Map Restart] [Player {client.PlayerName}] restart time is ended");
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Map restart time is ended! You cannot restart map until new map loaded."));
                return;
            }

            if(votedPlayers.Contains(client)) {
                SimpleLogging.LogDebug($"[Vote Map Restart] [Player {client.PlayerName}] already voted.");
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You have already voted!"));
                return;
            }


            votedPlayers.Add(client);
            SimpleLogging.LogDebug($"[Vote Map Restart] [Player {client.PlayerName}] voted to restart map.");
            playersRequiredToRestart = (int)Math.Ceiling(Utilities.GetPlayers().Count(player => !player.IsBot && !player.IsHLTV) * PluginSettings.getInstance.m_CVVoteMapRestartThreshold.Value);

            SimpleLogging.LogTrace($"[Vote Map Restart] [Player {client.PlayerName}] players count: {votedPlayers.Count}, Requires to restart: {playersRequiredToRestart}");
            Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"{client.PlayerName} wants to restart the map! Type !vmr in chat to vote. ({votedPlayers.Count} votes, {playersRequiredToRestart} required)"));

            if(votedPlayers.Count < playersRequiredToRestart)
                return;

            InitiateMapRestart();
        }

        private void InitiateMapRestart() {
            SimpleLogging.LogDebug("[Vote Map Restart] Initiating map restart...");
            isMapRestarting = true;
            Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"Vote successful! Map will be reload in {PluginSettings.getInstance.m_CVVoteMapRestartRestartTime.Value} seconds."));
            m_CSSPlugin.AddTimer(PluginSettings.getInstance.m_CVVoteMapRestartRestartTime.Value, () => {
                SimpleLogging.LogDebug("[Vote Map Restart] Changing map.");
                Server.ExecuteCommand($"changelevel {Server.MapName}");
            }, TimerFlags.STOP_ON_MAPCHANGE);
        }

        private HookResult OnMapTransition(EventMapTransition @event, GameEventInfo info) {
            votedPlayers.Clear();
            return HookResult.Continue;
        }

        private HookResult OnMapFullyLoaded(EventGameNewmap @event, GameEventInfo info) {
            mapStartTime = Server.EngineTime;
            return HookResult.Continue;
        }
    }
}