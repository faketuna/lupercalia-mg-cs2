using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;

namespace LupercaliaMGCore {
    public class RoundEndDeathMatch {
        private LupercaliaMGCore m_CSSPlugin;

        private ConVar? mp_teammates_are_enemies = null;

        public RoundEndDeathMatch(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            mp_teammates_are_enemies = ConVar.Find("mp_teammates_are_enemies");

            m_CSSPlugin.RegisterEventHandler<EventRoundPrestart>(OnRoundPreStart);
            m_CSSPlugin.RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        }

        private HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info) {
            SimpleLogging.LogDebug("[Round End Death Match] Called RoundPreStart.");
            if(!PluginSettings.getInstance.m_CVIsRoundEndDeathMatchEnabled.Value) {
                SimpleLogging.LogDebug("[Round End Death Match] REDM is disabled doing nothing.");
                return HookResult.Continue;
            }
            mp_teammates_are_enemies?.SetValue("false");
            SimpleLogging.LogDebug("[Round End Death Match] Ended.");
            return HookResult.Continue;
        }

        private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info) {
            SimpleLogging.LogDebug("[Round End Death Match] Called RoundEnd");
            if(!PluginSettings.getInstance.m_CVIsRoundEndDeathMatchEnabled.Value) {
                SimpleLogging.LogDebug("[Round End Death Match] REDM is disabled doing nothing.");
                return HookResult.Continue;
            }
            mp_teammates_are_enemies?.SetValue("true");
            SimpleLogging.LogDebug("[Round End Death Match] Started.");
            return HookResult.Continue;
        }
    }
}