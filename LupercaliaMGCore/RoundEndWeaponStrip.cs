using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class RoundEndWeaponStrip {
        private LupercaliaMGCore m_CSSPlugin;

        public RoundEndWeaponStrip(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
        }

        public void initialize() {
            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart, HookMode.Pre);
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {
            if(!PluginSettings.getInstance.m_CVIsRoundEndWeaponStripEnabled.Value)
                return HookResult.Continue;

            foreach(var player in Utilities.GetPlayers()) {
                if(player.IsBot || player.IsHLTV)
                    continue;

                player.RemoveWeapons();
            }
            return HookResult.Continue;
        }
    }
}