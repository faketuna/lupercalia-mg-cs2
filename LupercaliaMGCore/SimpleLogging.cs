using CounterStrikeSharp.API;

namespace LupercaliaMGCore {
    public static class SimpleLogging {
        public static void LogDebug(string information) {
            Server.PrintToConsole(information);
        }

        public static void LogTrace(string information) {
            Server.PrintToConsole(information);
        }
    }
}