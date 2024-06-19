using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class Respawn {
        private LupercaliaMGCore m_CSSPlugin;
        private bool repeatKillDetected = false;
        private Dictionary<int ,double> playerLastRespawnTime = new Dictionary<int, double>();

        public Respawn(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
            m_CSSPlugin.RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);
            m_CSSPlugin.RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        }

        private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info) {
            if(!PluginSettings.getInstance.m_CVAutoRespawnEnabled.Value || repeatKillDetected)
                return HookResult.Continue;
            
            var player = @event.Userid;

            if(player == null)
                return HookResult.Continue;

            if(player.IsBot || player.IsHLTV)
                return HookResult.Continue;

            SimpleLogging.LogDebug($"[Respawn] [Player {player.PlayerName}] Trying to respawn.");

            int index = (int)player.Index;

            if(!playerLastRespawnTime.ContainsKey(index)) {
                playerLastRespawnTime[index] = 0.0D;
            }

            if(Server.EngineTime - playerLastRespawnTime[index] <= PluginSettings.getInstance.m_CVAutoRespawnSpawnKillingDetectionTime.Value) {
                repeatKillDetected = true;
                SimpleLogging.LogDebug($"[Respawn] [Player {player.PlayerName}] Repeat kill is detected.");
                Server.PrintToChatAll(LupercaliaMGCore.MessageWithPrefix($"{ChatColors.Green}Repeat kill detected! {ChatColors.Default}Respawn is {ChatColors.DarkRed}disabled{ChatColors.Default} in this round."));
                return HookResult.Continue;
            }

            SimpleLogging.LogDebug($"[Respawn] [Player {player.PlayerName}] Respawning player.");
            m_CSSPlugin.AddTimer(PluginSettings.getInstance.m_CVAutoRespawnSpawnTime.Value, () => {
                SimpleLogging.LogDebug($"[Respawn] [Player {player.PlayerName}] Respawned.");
                respawnPlayer(player);
            }, TimerFlags.STOP_ON_MAPCHANGE);

            SimpleLogging.LogDebug($"[Respawn] [Player {player.PlayerName}] Done.");
            return HookResult.Continue;
        }

        
        private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info) {
            if(!PluginSettings.getInstance.m_CVAutoRespawnEnabled.Value || repeatKillDetected)
                return HookResult.Continue;

            var player = @event.Userid;

            if(player == null)
                return HookResult.Continue;

            if(player.IsBot || player.IsHLTV)
                return HookResult.Continue;

            int index = (int)player.Index;

            if(!playerLastRespawnTime.ContainsKey(index)) {
                playerLastRespawnTime[index] = 0.0D;
            }

            playerLastRespawnTime[index] = Server.EngineTime;
            return HookResult.Continue;
        }

        private void respawnPlayer(CCSPlayerController client) {
            if(client.Team == CsTeam.None || client.Team == CsTeam.Spectator)
                return;

            client.Respawn();
            client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You have been Auto-Respawned!"));
        }
    }
}