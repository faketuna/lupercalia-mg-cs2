using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class LupercaliaMGCore: BasePlugin
    {
        public static readonly string PLUGIN_PREFIX  = "[LPŘ MG]";

        public override string ModuleName => "Lupercalia MG Core";

        public override string ModuleVersion => "0.0.1";

        public override string ModuleAuthor => "faketuna";

        public override string ModuleDescription => "Provides core MG feature in CS2 with CounterStrikeSharp";

        // Feature classes
        private TeamBasedBodyColor? teamBasedBodyColor;
        private DuckFix? duckFix;
        private TeamScramble? teamScramble;



        public override void Load(bool hotReload)
        {
            teamBasedBodyColor = new TeamBasedBodyColor(this);
            teamBasedBodyColor.initialize();

            duckFix = new DuckFix(this, hotReload);
            duckFix.initialize();

            teamScramble = new TeamScramble(this);
            teamScramble.initialize();
        }
    }
}