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
        }

        public void initialize() {
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
            
            if(isRoundRestarting) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Round is already restarting in progress!"));
                return;
            }

            if(votedPlayers.Contains(client)) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You have already voted!"));
                return;
            }

            votedPlayers.Add(client);
            playersRequiredToRestart = (int)Math.Ceiling(votedPlayers.Count * PluginSettings.getInstance.m_CVVoteRoundRestartThreshold.Value);

            Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"{client.PlayerName} wants to restart the round! Type !vrr in chat to vote. ({votedPlayers.Count} votes, {playersRequiredToRestart} required)"));

            if(votedPlayers.Count < playersRequiredToRestart)
                return;
            
            InitiateRoundRestart();
        }

        private void InitiateRoundRestart() {
            isRoundRestarting = true;

            Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"Vote successful! Round will be reload in {PluginSettings.getInstance.m_CVVoteRoundRestartRestartTime.Value} seconds."));
            m_CSSPlugin.AddTimer(PluginSettings.getInstance.m_CVVoteRoundRestartRestartTime.Value, () => {
                //                                                                                                           Delay seems not working?
                Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").First().GameRules?.TerminateRound(0.0F, RoundEndReason.RoundDraw);
            }, TimerFlags.STOP_ON_MAPCHANGE);
        }
    }
}