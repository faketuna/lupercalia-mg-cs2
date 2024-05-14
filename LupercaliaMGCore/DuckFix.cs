using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    

    public class DuckFix
    {
        private LupercaliaMGCore m_CSSPlugin;
        private bool isHotReload;

        public DuckFix(LupercaliaMGCore plugin, bool hotReload) {
            m_CSSPlugin = plugin;
            isHotReload = hotReload;
        }
        
        public void initialize() {
            m_CSSPlugin.RegisterEventHandler<EventPlayerConnectFull>(onPlayerConnected);

            if(isHotReload) {
                List<CCSPlayerController> players = Utilities.GetPlayers();
                foreach (CCSPlayerController player in players) {
                    if(player == null || !player.IsValid)
                        continue;

                    CCSPlayer_MovementServices movementServices = new CCSPlayer_MovementServices(player.PlayerPawn.Value!.MovementServices!.Handle);
                    m_CSSPlugin.RegisterListener<Listeners.OnTick>(() => 
                    {
                        if(movementServices != null) {
                            movementServices.LastDuckTime = 0.0f;
                            movementServices.DuckSpeed = 8.0f;
                        }
                    });
                }
            }
        }

        private HookResult onPlayerConnected(EventPlayerConnectFull @event, GameEventInfo info) {
            CCSPlayerController player = @event.Userid!;
            CCSPlayer_MovementServices movementServices = new CCSPlayer_MovementServices(player.PlayerPawn.Value!.MovementServices!.Handle);
            m_CSSPlugin.RegisterListener<Listeners.OnTick>(() => 
            {
                if(movementServices != null) {
                    movementServices.LastDuckTime = 0.0f;
                    movementServices.DuckSpeed = 8.0f;
                }
            });
            return HookResult.Continue;
        }
    }
}