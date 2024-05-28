using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class LupercaliaMGCore: BasePlugin
    {
        public static readonly string PLUGIN_PREFIX  = $" {ChatColors.DarkRed}[{ChatColors.Blue}LPŘ MG{ChatColors.DarkRed}]{ChatColors.Default}";

        public static string MessageWithPrefix(string message) {
            return $"{PLUGIN_PREFIX} {message}";
        }

        private static LupercaliaMGCore? instance;

        public static LupercaliaMGCore getInstance() {
            return instance!;
        }

        public override string ModuleName => "Lupercalia MG Core";

        public override string ModuleVersion => "0.1.0";

        public override string ModuleAuthor => "faketuna";

        public override string ModuleDescription => "Provides core MG feature in CS2 with CounterStrikeSharp";


        public override void Load(bool hotReload)
        {
            instance = this;
            new PluginSettings(this);

            new TeamBasedBodyColor(this);
            new DuckFix(this, hotReload);
            new TeamScramble(this);
            new VoteMapRestart(this);
            new VoteRoundRestart(this);
            new RoundEndDamageImmunity(this);
            new RoundEndWeaponStrip(this);
            new ScheduledShutdown(this);
            new Respawn(this);
            new MapConfig(this);
            // new AntiCamp(this, hotReload);
            new Omikuji(this);
        }
    }
}