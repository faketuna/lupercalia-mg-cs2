using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using System.Drawing;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Entities.Constants;

namespace LupercaliaMGCore {
    public class VoteRoundRestart
    {
        private LupercaliaMGCore m_CSSPlugin;
        private List<CCSPlayerController> votedPlayers = new List<CCSPlayerController>();
        private int playersRequiredToRestart = 0;
        private bool isRoundRestarting = false;

        public VoteRoundRestart(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.AddCommand("css_vrr", "Vote round restart command.", CommandVoteRestartRound);
            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart);
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {
            isRoundRestarting = false;
            votedPlayers.Clear();
            return HookResult.Continue;
        }
        private void CommandVoteRestartRound(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;
            
            SimpleLogging.LogDebug($"[Vote Round Restart] [Player {client.PlayerName}] trying to vote for restart round.");
            if(isRoundRestarting) {
                SimpleLogging.LogDebug($"[Vote Round Restart] [Player {client.PlayerName}] Round is already restarting in progress.");
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Round is already restarting in progress!"));
                return;
            }

            if(votedPlayers.Contains(client)) {
                SimpleLogging.LogDebug($"[Vote Round Restart] [Player {client.PlayerName}] trying to vote for restart round.");
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You have already voted!"));
                return;
            }

            votedPlayers.Add(client);
            SimpleLogging.LogDebug($"[Vote Round Restart] [Player {client.PlayerName}] voted to restart round.");
            playersRequiredToRestart = (int)Math.Ceiling(Utilities.GetPlayers().Count(player => !player.IsBot && !player.IsHLTV) * PluginSettings.getInstance.m_CVVoteRoundRestartThreshold.Value);

            SimpleLogging.LogDebug($"[Vote Round Restart] [Player {client.PlayerName}] players count: {votedPlayers.Count}, Requires to restart: {playersRequiredToRestart}");
            Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"{client.PlayerName} wants to restart the round! Type !vrr in chat to vote. ({votedPlayers.Count} votes, {playersRequiredToRestart} required)"));

            if(votedPlayers.Count < playersRequiredToRestart)
                return;
            
            InitiateRoundRestart();
        }

        private void InitiateRoundRestart() {
            SimpleLogging.LogDebug("[Vote Round Restart] Initiating round restart...");
            isRoundRestarting = true;

            Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"Vote successful! Round will be reload in {PluginSettings.getInstance.m_CVVoteRoundRestartRestartTime.Value} seconds."));
            m_CSSPlugin.AddTimer(PluginSettings.getInstance.m_CVVoteRoundRestartRestartTime.Value, () => {
                SimpleLogging.LogDebug("[Vote Round Restart] Restarting round.");
                Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").First().GameRules?.TerminateRound(0.0F, RoundEndReason.RoundDraw);
            }, TimerFlags.STOP_ON_MAPCHANGE);
        }
    }
}