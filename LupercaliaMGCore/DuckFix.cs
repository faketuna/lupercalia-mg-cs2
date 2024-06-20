using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace LupercaliaMGCore {
    

    public class DuckFix
    {
        private LupercaliaMGCore m_CSSPlugin;

        public DuckFix(LupercaliaMGCore plugin, bool hotReload) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.RegisterEventHandler<EventPlayerConnectFull>(onPlayerConnected);

            if(hotReload) {
                List<CCSPlayerController> players = Utilities.GetPlayers();
                foreach (CCSPlayerController player in players) {
                    if(player == null || !player.IsValid)
                        continue;

                    hookPlayerMovement(player);
                }
            }
        }

        private HookResult onPlayerConnected(EventPlayerConnectFull @event, GameEventInfo info) {
            CCSPlayerController player = @event.Userid!;
            hookPlayerMovement(player);
            return HookResult.Continue;
        }

        private void hookPlayerMovement(CCSPlayerController player) {
            m_CSSPlugin.RegisterListener<Listeners.OnTick>(() => 
            {
                if((player.Buttons & PlayerButtons.Duck) == 0) 
                    return;

                CCSPlayer_MovementServices movementServices = new CCSPlayer_MovementServices(player.PlayerPawn.Value!.MovementServices!.Handle);
                if(movementServices != null) {
                    movementServices.LastDuckTime = 0.0f;
                    movementServices.DuckSpeed = 8.0f;
                }
            });
        }
    }
}