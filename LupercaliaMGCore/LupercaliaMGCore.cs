using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class LupercaliaMGCore: BasePlugin
    {
        public static readonly string PLUGIN_PREFIX  = $" {ChatColors.DarkRed}[{ChatColors.Blue}LPŘ MG{ChatColors.DarkRed}]{ChatColors.Default}";

        public static string MessageWithPrefix(string message) {
            return $"{PLUGIN_PREFIX} {message}";
        }

        public override string ModuleName => "Lupercalia MG Core";

        public override string ModuleVersion => "0.0.1";

        public override string ModuleAuthor => "faketuna";

        public override string ModuleDescription => "Provides core MG feature in CS2 with CounterStrikeSharp";

        // Feature classes
        private TeamBasedBodyColor? teamBasedBodyColor;
        private DuckFix? duckFix;
        private TeamScramble? teamScramble;
        private VoteMapRestart? voteMapRestart;
        private VoteRoundRestart? voteRoundRestart;
        private RoundEndDamageImmunity? roundEndDamageImmunity;
        private RoundEndWeaponStrip? roundEndWeaponStrip;


        public override void Load(bool hotReload)
        {
            new PluginSettings(this);

            teamBasedBodyColor = new TeamBasedBodyColor(this);
            teamBasedBodyColor.initialize();

            duckFix = new DuckFix(this, hotReload);
            duckFix.initialize();

            teamScramble = new TeamScramble(this);
            teamScramble.initialize();

            voteMapRestart = new VoteMapRestart(this);
            voteMapRestart.initialize();

            voteRoundRestart = new VoteRoundRestart(this);
            voteRoundRestart.initialize();

            roundEndDamageImmunity = new RoundEndDamageImmunity(this);
            roundEndDamageImmunity.initialize();

            roundEndWeaponStrip = new RoundEndWeaponStrip(this);
            roundEndWeaponStrip.initialize();
        }
    }
}