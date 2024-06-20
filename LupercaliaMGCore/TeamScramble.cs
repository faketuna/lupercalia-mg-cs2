using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    

    public class TeamScramble
    {
        private LupercaliaMGCore m_CSSPlugin;

        private static Random random = new Random();

        public TeamScramble(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        }

        private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info) {
            if(!PluginSettings.getInstance.m_CVIsScrambleEnabled.Value)
                return HookResult.Continue;

            SimpleLogging.LogDebug("[Team Scramble] Called");

            List<CCSPlayerController> players = Utilities.GetPlayers();

            int playerCount = players.Count;
            int playerCountHalf = playerCount/2;
            SimpleLogging.LogTrace($"[Team Scramble] player count: {playerCount}, half: {playerCountHalf}");

            int teamCountCT = 0;
            int teamCountT = 0;

            foreach(var client in players) {
                int randomTeam = random.Next(0, 5000);
                if(randomTeam >= 2500) {
                    if(teamCountCT >= playerCountHalf) {
                        SimpleLogging.LogTrace($"Player {client.PlayerName} moved to Terrorist");
                        client.SwitchTeam(CsTeam.Terrorist);
                    }
                    else {
                        SimpleLogging.LogTrace($"Player {client.PlayerName} moved to CT");
                        client.SwitchTeam(CsTeam.CounterTerrorist);
                        teamCountCT++;
                    }
                }
                else {
                    if(teamCountT >= playerCountHalf) {
                        SimpleLogging.LogTrace($"Player {client.PlayerName} moved to CT");
                        client.SwitchTeam(CsTeam.CounterTerrorist);
                    }
                    else {
                        SimpleLogging.LogTrace($"Player {client.PlayerName} moved to Terrorist");
                        client.SwitchTeam(CsTeam.Terrorist);
                        teamCountT++;
                    }
                }
            }

            SimpleLogging.LogDebug("[Team Scramble] Done");
            return HookResult.Continue;
        }
    }
}