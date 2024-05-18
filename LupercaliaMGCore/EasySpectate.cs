using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class EasySpectate {
        private LupercaliaMGCore m_CSSPlugin;
        private Dictionary<CCSPlayerController ,double> playerLastButtonPressedTime = new Dictionary<CCSPlayerController, double>();
        private readonly double buttonCooldown = 1.0D;

        public EasySpectate(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
        }

        private void hookPlayerButton(CCSPlayerController player) {
            if(player == null)
                return;

            playerLastButtonPressedTime[player] = Server.EngineTime;

            m_CSSPlugin.RegisterListener<Listeners.OnTick>(() => {
                if(player.Buttons != PlayerButtons.Use)
                    return;
                
                if(Server.EngineTime - playerLastButtonPressedTime[player] < buttonCooldown)
                    return;
                
                // TODO. Will be implemented after Trace Ray feature implemented in CounterStrikeSharp.
            });
        }
    }
}