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
            m_CSSPlugin.RegisterListener<Listeners.OnTick>(() => 
            {
                foreach(CCSPlayerController client in Utilities.GetPlayers()) {
                    if(!client.IsValid || client.IsBot || client.IsHLTV)
                        continue;

                    if((client.Buttons & PlayerButtons.Duck) == 0) 
                        return;

                    CCSPlayerPawn? playerPawn = client.PlayerPawn.Value;

                    if(playerPawn == null)
                        return;

                    CPlayer_MovementServices? pmService = playerPawn.MovementServices;

                    if(pmService == null) 
                        return;

                    CCSPlayer_MovementServices movementServices = new CCSPlayer_MovementServices(pmService.Handle);
                    if(movementServices != null) {
                        movementServices.LastDuckTime = 0.0f;
                        movementServices.DuckSpeed = 8.0f;
                    }

                }
            });
        }
    }
}