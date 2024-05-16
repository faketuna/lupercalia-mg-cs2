using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class Respawn {
        private LupercaliaMGCore m_CSSPlugin;
        private bool repeatKillDetected = false;

        public Respawn(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
            m_CSSPlugin.RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);
        }

        private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info) {
            if(!PluginSettings.getInstance.m_CVAutoRespawnEnabled.Value || repeatKillDetected)
                return HookResult.Continue;
            
            var player = @event.Userid;

            if(player == null)
                return HookResult.Continue;

            if(player.IsBot || player.IsHLTV)
                return HookResult.Continue;

            m_CSSPlugin.AddTimer(PluginSettings.getInstance.m_CVAutoRespawnSpawnTime.Value, () => {
                respawnPlayer(player);
            }, TimerFlags.STOP_ON_MAPCHANGE);

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