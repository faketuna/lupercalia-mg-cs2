using CounterStrikeSharp.API;

namespace LupercaliaMGCore {
    public static class SimpleLogging {
        public static void LogDebug(string information) {
            if(PluginSettings.getInstance.m_CVPluginDebugLevel.Value < 1) {
                return;
            }
            if(PluginSettings.getInstance.m_CVPluginDebugShowChat.Value) {
                Server.PrintToChatAll("[LPR MG DEBUG] " + information);
            }
            Server.PrintToConsole("[LPR MG DEBUG] " + information);
        }

        public static void LogTrace(string information) {
            if(PluginSettings.getInstance.m_CVPluginDebugLevel.Value < 2) {
                return;
            }
            if(PluginSettings.getInstance.m_CVPluginDebugShowChat.Value) {
                Server.PrintToChatAll("[LPR MG TRACE] " + information);
            }
            Server.PrintToConsole("[LPR MG TRACE] " + information);
        }
    }
}