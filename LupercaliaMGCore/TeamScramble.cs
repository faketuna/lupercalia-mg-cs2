using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    

    public class TeamScramble
    {
        private LupercaliaMGCore m_CSSPlugin;

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
            List<uint> pickedPlayer = new List<uint>();

            SimpleLogging.LogTrace($"[Team Scramble] player count: {playerCount}, half: {playerCountHalf}");

            var unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Random random = new Random(unixTimestamp);

            SimpleLogging.LogDebug($"[Team Scramble] Iterating half of players to join CounterTerrorist force");
            for(int i = playerCountHalf; i > 0; i--) {
                if(pickedPlayer.Contains(players[i].Index)) {
                    i++;
                    continue;
                }

                int index = random.Next(playerCount);
                pickedPlayer.Add(players[index].Index);
                players[index].SwitchTeam(CsTeam.CounterTerrorist);
                SimpleLogging.LogTrace($"[Team Scramble] Player {players[index].PlayerName} is moved to CounterTerrorist");
            }

            for(int i = playerCount-1; i >= 0; i--) {
                if(pickedPlayer.Contains(players[i].Index)) 
                    continue;
                
                players[i].SwitchTeam(CsTeam.Terrorist);
                SimpleLogging.LogTrace($"[Team Scramble] Player {players[i].PlayerName} is moved to Terrorist");
            }
            

            SimpleLogging.LogDebug("[Team Scramble] Done");
            return HookResult.Continue;
        }
    }
}