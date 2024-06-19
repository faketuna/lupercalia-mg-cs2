using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class RoundEndWeaponStrip {
        private LupercaliaMGCore m_CSSPlugin;

        public RoundEndWeaponStrip(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
            
            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart, HookMode.Pre);
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {
            if(!PluginSettings.getInstance.m_CVIsRoundEndWeaponStripEnabled.Value)
                return HookResult.Continue;

            SimpleLogging.LogDebug("[Round End Weapon Strip] Removing all players weapons.");
            foreach(var player in Utilities.GetPlayers()) {
                if(player.IsBot || player.IsHLTV)
                    continue;

                player.RemoveWeapons();
            }
            SimpleLogging.LogDebug("[Round End Weapon Strip] Done.");
            return HookResult.Continue;
        }
    }
}