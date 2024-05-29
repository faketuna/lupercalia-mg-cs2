using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Cvars.Validators;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class PluginSettings {

        private static PluginSettings? settingsInstance;

        public const string CONFIG_FOLDER = "csgo/cfg/lupercalia/";
        private const string CONFIG_FILE = "mgcore.cfg";

        public static PluginSettings getInstance {
            get {
                if(settingsInstance == null)
                    throw new InvalidOperationException("Settings instance is not initialized yet.");
                
                return settingsInstance;
            }
        }

        /*
        *   Team based body color
        */
        public FakeConVar<string> m_CVTeamColorCT  = new("lp_mg_teamcolor_ct", "Counter Terrorist's Body color. R, G, B", "0, 0, 255");
        public FakeConVar<string> m_CVTeamColorT = new("lp_mg_teamcolor_t", "Terrorist's Body color. R, G, B", "255, 0, 0");

        /*
        *   Team scramble
        */
        public FakeConVar<bool> m_CVIsScrambleEnabled = new("lp_mg_teamscramble_enabled", "Should team is scrambled after round end", true);

        /*
        *   Vote round/map restart
        */
        public FakeConVar<double> m_CVVoteMapRestartAllowedTime = new("lp_mg_vmr_allowed_time", "How long allowed to use vote command after map loaded in seconds.", 60.0D);
        public FakeConVar<float> m_CVVoteMapRestartThreshold = new("lp_mg_vmr_vote_threshold", "How percent of votes required to initiate the map restart.", 0.7F, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0.0F, 1.0F));
        public FakeConVar<float> m_CVVoteMapRestartRestartTime = new("lp_mg_vmr_restart_time", "How long to take restarting map after vote passed.", 10.0F, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0.0F, float.MaxValue));
        public FakeConVar<float> m_CVVoteRoundRestartThreshold = new("lp_mg_vrr_vote_threshold", "How percent of votes required to initiate the round restart.", 0.7F, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0.0F, 1.0F));
        public FakeConVar<float> m_CVVoteRoundRestartRestartTime = new("lp_mg_vrr_restart_time", "How long to take restarting round after vote passed.", 10.0F, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0.0F, float.MaxValue));

        /*
        *   Round end enhancement
        */
        public FakeConVar<bool> m_CVIsRoundEndDamageImmunityEnabled = new("lp_mg_redi_enabled", "Should player grant damage immunity after round end until next round starts.", true);
        public FakeConVar<bool> m_CVIsRoundEndWeaponStripEnabled = new("lp_mg_rews_enabled", "Should player's weapons are removed before new round starts.", true);

        /*
        *   Scheduled shutdown
        */
        public FakeConVar<string> m_CVScheduledShutdownTime = new("lp_mg_scheduled_shutdown_time", "Server will be shutdown in specified time. Format is HHmm", "0500");
        public FakeConVar<int> m_CVScheduledShutdownWarningTime = new("lp_mg_scheduled_shutdown_warn_time", "Show shutdown warning countdown if lp_mg_scheduled_shutdown_round_end is false.", 10);
        public FakeConVar<bool> m_CVScheduledShutdownRoundEnd = new("lp_mg_scheduled_shutdown_round_end", "When set to true server will be shutdown after round end.", true);

        /*
        *   Auto respawn
        */
        public FakeConVar<bool> m_CVAutoRespawnEnabled = new("lp_mg_auto_respawn_enabled", "Auto respawn feature is enabled", false);
        public FakeConVar<float> m_CVAutoRespawnSpawnKillingDetectionTime = new("lp_mg_auto_respawn_repeat_kill_time", "Seconds to detect as spawn killing.", 1.0F);
        public FakeConVar<float> m_CVAutoRespawnSpawnTime = new("lp_mg_auto_respawn_time", "How long to respawn after death.", 1.0F);

        /*
        *   Anti camp
        */
        public FakeConVar<bool> m_CVAntiCampEnabled = new("lp_mg_anti_camp_enabled", "Anti camp enabled", true);
        public FakeConVar<float> m_CVAntiCampDetectionTime = new("lp_mg_anti_camp_detection_time", "How long to take detected as camping in seconds.", 10.0F);
        public FakeConVar<double> m_CVAntiCampDetectionRadius = new("lp_mg_anti_camp_detection_radius", "Range of area for player should move for avoiding the detected as camping.", 200.0F);
        public FakeConVar<float> m_CVAntiCampDetectionInterval = new("lp_mg_anti_camp_detection_interval", "Interval to run camping check in seconds.", 0.1F);
        public FakeConVar<float> m_CVAntiCampMarkingTime = new("lp_mg_anti_camp_glowing_time", "How long to detected player keep glowing.", 10.0F);

        /*
        *   Map config
        */
        public FakeConVar<int> m_CVMapConfigExecutionTiming = new("lp_mg_mapcfg_execution_timing", "When configs are executed? 0: Does nothing, 1: Execute on map start, 2: Execute on every round start, 3: Execute on map transition and every round start", 1, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(0, 3));
        public FakeConVar<int> m_CVMapConfigType = new ("lp_mg_mapcfg_type", "Map configuration type. 0: disabled, 1: Exact match, 2: Partial Match", 1, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(0, 2));

        /*
        *   Omikuji
        */
        public FakeConVar<int> m_CVOmikujiEventWeightMisc = new("lp_mg_omikuji_event_weight_misc", "Weight of misc event. You can set to any value.", 90);
        public FakeConVar<int> m_CVOmikujiEventWeightBad = new("lp_mg_omikuji_event_weight_bad", "Weight of bad event. You can set to any value.", 5);
        public FakeConVar<int> m_CVOmikujiEventWeightLucky = new("lp_mg_omikuji_event_weight_lucky", "Weight of lucky event. You can set to any value.", 5);

        /*
        *   Omikuji - Chicken
        */
        public FakeConVar<float> m_CVOmikujiEventChickenTime = new("lp_mg_omikuji_event_chicken_time", "How long that chicken alive?", 5.0F);
        public FakeConVar<float> m_CVOmikujiEventChickenBodyScale = new("lp_mg_omikuji_event_chicken_body_scale", "Body size of the chicken. Default size is 1.0", 1.0F);

        private LupercaliaMGCore m_CSSPlugin;

        public PluginSettings(LupercaliaMGCore plugin) {
            settingsInstance = this;
            m_CSSPlugin = plugin;
            initializeSettings();
            m_CSSPlugin.RegisterFakeConVars(typeof(PluginSettings), this);
        }

        public bool initializeSettings() {
            string configFolder = Path.Combine(Server.GameDirectory, CONFIG_FOLDER);

            if(!Directory.Exists(configFolder)) {
                m_CSSPlugin.Logger.LogInformation($"Failed to find the config folder. Trying to generate...");

                Directory.CreateDirectory(configFolder);

                if(!Directory.Exists(configFolder)) {
                    m_CSSPlugin.Logger.LogError($"Failed to generate the Config folder!");
                    return false;
                }
            }

            string configLocation = Path.Combine(configFolder, CONFIG_FILE);

            if(!File.Exists(configLocation)) {
                m_CSSPlugin.Logger.LogInformation($"Failed to find the config file. Trying to generate...");

                try {
                    generateCFG(configLocation);
                } catch(Exception e) {
                    m_CSSPlugin.Logger.LogError($"Failed to generate config file!\n{e.StackTrace}");
                    return false;
                }
                
                m_CSSPlugin.Logger.LogInformation($"Config file created.");
            }

            Server.ExecuteCommand($"exec lupercalia/{CONFIG_FILE}");
            return true;
        }

        private void generateCFG(string configPath) {
            StreamWriter config = File.CreateText(configPath);

            config.WriteLine($"{m_CVIsScrambleEnabled.Name} {Convert.ToInt32(m_CVIsScrambleEnabled.Value)}");
            config.WriteLine($"{m_CVMapConfigType.Name} {m_CVMapConfigType.Value}");
            config.WriteLine($"{m_CVTeamColorCT.Name} {m_CVTeamColorCT.Value}");
            config.WriteLine($"{m_CVTeamColorT.Name} {m_CVTeamColorT.Value}");
            config.WriteLine($"{m_CVVoteMapRestartAllowedTime.Name} {m_CVVoteMapRestartAllowedTime.Value}");
            config.WriteLine($"{m_CVVoteMapRestartThreshold.Name} {m_CVVoteMapRestartThreshold.Value}");
            config.WriteLine($"{m_CVVoteMapRestartRestartTime.Name} {m_CVVoteMapRestartRestartTime.Value}");
            config.WriteLine($"{m_CVVoteRoundRestartThreshold.Name} {m_CVVoteRoundRestartThreshold.Value}");
            config.WriteLine($"{m_CVVoteRoundRestartRestartTime.Name} {m_CVVoteRoundRestartRestartTime.Value}");
            config.WriteLine($"{m_CVIsRoundEndDamageImmunityEnabled.Name} {m_CVIsRoundEndDamageImmunityEnabled.Value}");
            config.WriteLine($"{m_CVIsRoundEndWeaponStripEnabled.Name} {m_CVIsRoundEndWeaponStripEnabled.Value}");
            config.WriteLine($"{m_CVAntiCampEnabled.Name} {m_CVAntiCampEnabled.Value}");
            config.WriteLine($"{m_CVAntiCampDetectionTime.Name} {m_CVAntiCampDetectionTime.Value}");
            config.WriteLine($"{m_CVAntiCampDetectionRadius.Name} {m_CVAntiCampDetectionRadius.Value}");
            config.WriteLine($"{m_CVAntiCampDetectionInterval.Name} {m_CVAntiCampDetectionInterval.Value}");
            config.WriteLine($"{m_CVAntiCampMarkingTime.Name} {m_CVAntiCampMarkingTime.Value}");
            config.WriteLine($"{m_CVMapConfigExecutionTiming.Name} {m_CVMapConfigExecutionTiming.Value}");
            config.WriteLine($"{m_CVOmikujiEventWeightMisc.Name} {m_CVOmikujiEventWeightMisc.Value}");
            config.WriteLine($"{m_CVOmikujiEventWeightBad.Name} {m_CVOmikujiEventWeightBad.Value}");
            config.WriteLine($"{m_CVOmikujiEventWeightLucky.Name} {m_CVOmikujiEventWeightLucky.Value}");
            config.WriteLine($"{m_CVOmikujiEventChickenTime.Name} {m_CVOmikujiEventChickenTime.Value}");
            config.WriteLine($"{m_CVOmikujiEventChickenBodyScale.Name} {m_CVOmikujiEventChickenBodyScale.Value}");

            config.Close();
        }
    }
}