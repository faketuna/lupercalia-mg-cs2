using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class RoundEndDamageImmunity {
        private LupercaliaMGCore m_CSSPlugin;

        private bool damageImmunity = false;

        public RoundEndDamageImmunity(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt, HookMode.Pre);
            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart);
            m_CSSPlugin.RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        }

        private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info) {
            if(damageImmunity && PluginSettings.getInstance.m_CVIsRoundEndDamageImmunityEnabled.Value) {
                var player = @event.Userid?.PlayerPawn?.Value;

                if(player == null)
                    return HookResult.Continue;

                player.Health = player.LastHealth;
                SimpleLogging.LogTrace($"[Round End Damage Immunity] [Player {player.Controller.Value!.PlayerName}] Nullified damage");
                return HookResult.Continue;

            }
            return HookResult.Continue;
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {
            damageImmunity = false;
            SimpleLogging.LogDebug("[Round End Damage Immunity] Disabled.");
            return HookResult.Continue;
        }

        private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info) {
            damageImmunity = true;
            SimpleLogging.LogDebug("[Round End Damage Immunity] Enabled.");
            return HookResult.Continue;
        }
    }
}