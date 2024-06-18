using System.Runtime.CompilerServices;
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

        /*
        *   Omikuji - Screen shake
        */
        public FakeConVar<float> m_CVOmikujiEventScreenShakeAmplitude = new("lp_mg_omikuji_event_screen_shake_amplitude", "How far away from the normal position the camera will wobble. Should be a range between 0 and 16.", 1000.0F);
        public FakeConVar<float> m_CVOmikujiEventScreenShakeDuration = new("lp_mg_omikuji_event_screen_shake_duration", "The length of time in which to shake the player's screens.", 5.0F);
        public FakeConVar<float> m_CVOmikujiEventScreenShakeFrequency = new("lp_mg_omikuji_event_screen_shake_frequency", "How many times per second to change the direction of the camera wobble. 40 is generally enough; values higher are hardly distinguishable.", 1000.0F);

        /*
        *   Omikuji - Player Heal
        */
        public FakeConVar<int> m_CVOmikujiEventPlayerHeal = new("lp_mg_omikuji_event_player_heal_amount", "How many health healed when event occur", 100);

        /*
        *   Omikuji - Gravity
        */
        public FakeConVar<int> m_CVOmikujiEventGravityMax = new("lp_mg_omikuji_event_gravity_max", "Maximum value of sv_gravity", 800);
        public FakeConVar<int> m_CVOmikujiEventGravityMin = new("lp_mg_omikuji_event_gravity_min", "Minimal value of sv_gravity", 100);
        public FakeConVar<float> m_CVOmikujiEventGravityRestoreTime = new("lp_mg_omikuji_event_gravity_restore_time", "How long to take gravity restored in seconds.", 10.0F);

        /*
        *   Omikuji - Gravity
        */
        public FakeConVar<float> m_CVOmikujiEventPlayerFreeze = new("lp_mg_omikuji_event_player_freeze_time", "How long to player freeze in seconds.", 3.0F);

        /*
        *   Omikuji - GiveRandomItem
        */
        public FakeConVar<int> m_CVOmikujiEventGiveRandomItemAvoidCount = new("lp_mg_omikuji_event_give_random_item_avoid_duplication_history", "How many histories save to avoid give duplicated item.", 10);

        /*
        *   Omikuji - Player Slap
        */
        public FakeConVar<int> m_CVOmikujiEventPlayerSlapPowerMin = new("lp_mg_omikuji_event_player_slap_power_min", "Minimal power of slap.", 0);
        public FakeConVar<int> m_CVOmikujiEventPlayerSlapPowerMax = new("lp_mg_omikuji_event_player_slap_power_max", "Maximum power of slap.", 30000);


        private LupercaliaMGCore m_CSSPlugin;

        public PluginSettings(LupercaliaMGCore plugin) {
            plugin.Logger.LogDebug("Setting the instance info");
            settingsInstance = this;
            plugin.Logger.LogDebug("Setting the plugin instance");
            m_CSSPlugin = plugin;
            plugin.Logger.LogDebug("Initializing the settings");
            initializeSettings();
            plugin.Logger.LogDebug("Registering the fake convar");
            m_CSSPlugin.RegisterFakeConVars(typeof(PluginSettings), this);
        }

        public bool initializeSettings() {
            m_CSSPlugin.Logger.LogDebug("Generate path to config folder");
            string configFolder = Path.Combine(Server.GameDirectory, CONFIG_FOLDER);

            m_CSSPlugin.Logger.LogDebug("Checking existence of config folder");
            if(!Directory.Exists(configFolder)) {
                m_CSSPlugin.Logger.LogInformation($"Failed to find the config folder. Trying to generate...");

                Directory.CreateDirectory(configFolder);

                if(!Directory.Exists(configFolder)) {
                    m_CSSPlugin.Logger.LogError($"Failed to generate the Config folder!");
                    return false;
                }
            }

            m_CSSPlugin.Logger.LogDebug("Generate path to config file");
            string configLocation = Path.Combine(configFolder, CONFIG_FILE);

            m_CSSPlugin.Logger.LogDebug("Checking existence of config file");
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

            m_CSSPlugin.Logger.LogDebug("Executing config");
            Server.ExecuteCommand($"exec lupercalia/{CONFIG_FILE}");
            return true;
        }

        private void generateCFG(string configPath) {
            StreamWriter config = File.CreateText(configPath);

            /*
            *   Team based body color
            */
            writeConVarConfig(config, m_CVTeamColorCT);
            writeConVarConfig(config, m_CVTeamColorT);
            config.WriteLine("\n");


            /*
            *   Team scramble
            */
            writeConVarConfig(config, m_CVIsScrambleEnabled);
            config.WriteLine("\n");


            /*
            *   Vote round/map restart
            */
            writeConVarConfig(config, m_CVVoteMapRestartAllowedTime);
            writeConVarConfig(config, m_CVVoteMapRestartThreshold);
            writeConVarConfig(config, m_CVVoteMapRestartRestartTime);
            writeConVarConfig(config, m_CVVoteRoundRestartThreshold);
            writeConVarConfig(config, m_CVVoteRoundRestartRestartTime);
            config.WriteLine("\n");


            /*
            *   Round end enhancement
            */
            writeConVarConfig(config, m_CVIsRoundEndDamageImmunityEnabled);
            writeConVarConfig(config, m_CVIsRoundEndWeaponStripEnabled);
            config.WriteLine("\n");


            /*
            *   Scheduled shutdown
            */
            writeConVarConfig(config, m_CVScheduledShutdownTime);
            writeConVarConfig(config, m_CVScheduledShutdownWarningTime);
            writeConVarConfig(config, m_CVScheduledShutdownRoundEnd);
            config.WriteLine("\n");


            /*
            *   Auto respawn
            */
            writeConVarConfig(config, m_CVAutoRespawnEnabled);
            writeConVarConfig(config, m_CVAutoRespawnSpawnKillingDetectionTime);
            writeConVarConfig(config, m_CVAutoRespawnSpawnTime);
            config.WriteLine("\n");



            /*
            *   Anti camp
            */
            writeConVarConfig(config, m_CVAntiCampEnabled);
            writeConVarConfig(config, m_CVAntiCampDetectionTime);
            writeConVarConfig(config, m_CVAntiCampDetectionRadius);
            writeConVarConfig(config, m_CVAntiCampDetectionInterval);
            writeConVarConfig(config, m_CVAntiCampMarkingTime);
            config.WriteLine("\n");


            /*
            *   Map config
            */
            writeConVarConfig(config, m_CVMapConfigType);
            writeConVarConfig(config, m_CVMapConfigExecutionTiming);
            config.WriteLine("\n");


            /*
            *   Omikuji
            */
            writeConVarConfig(config, m_CVOmikujiEventWeightMisc);
            writeConVarConfig(config, m_CVOmikujiEventWeightBad);
            writeConVarConfig(config, m_CVOmikujiEventWeightLucky);
            config.WriteLine("\n");

            /*
            *   Omikuji - Chicken
            */
            writeConVarConfig(config, m_CVOmikujiEventChickenTime);
            writeConVarConfig(config, m_CVOmikujiEventChickenBodyScale);
            config.WriteLine("\n");

            /*
            *   Omikuji - Screen shake
            */
            writeConVarConfig(config, m_CVOmikujiEventScreenShakeAmplitude);
            writeConVarConfig(config, m_CVOmikujiEventScreenShakeDuration);
            writeConVarConfig(config, m_CVOmikujiEventScreenShakeFrequency);
            config.WriteLine("\n");

            /*
            *   Omikuji - Player Heal
            */
            writeConVarConfig(config, m_CVOmikujiEventPlayerHeal);
            config.WriteLine("\n");

            /*
            *   Omikuji - Gravity
            */
            writeConVarConfig(config, m_CVOmikujiEventGravityMax);
            writeConVarConfig(config, m_CVOmikujiEventGravityMin);
            writeConVarConfig(config, m_CVOmikujiEventGravityRestoreTime);
            config.WriteLine("\n");

            /*
            *   Omikuji - Gravity
            */
            writeConVarConfig(config, m_CVOmikujiEventPlayerFreeze);
            config.WriteLine("\n");

            /*
            *   Omikuji - GiveRandomItem
            */ 
            writeConVarConfig(config, m_CVOmikujiEventGiveRandomItemAvoidCount);
            config.WriteLine("\n");

            /*
            *   Omikuji - Player Slap
            */
            writeConVarConfig(config, m_CVOmikujiEventPlayerSlapPowerMin);
            writeConVarConfig(config, m_CVOmikujiEventPlayerSlapPowerMax);
            config.WriteLine("\n");

            config.Close();
        }

        private static void writeConVarConfig<T>(StreamWriter configFile, FakeConVar<T> convar)where T : IComparable<T>{
            configFile.WriteLine($"// {convar.Description}");
            if(typeof(T) == typeof(bool)) {
                var conValue = convar.Value;
                bool value = Unsafe.As<T, bool>(ref conValue);
                configFile.WriteLine($"{convar.Name} {Convert.ToInt32(value)}");
            } else {
                configFile.WriteLine($"{convar.Name} {convar.Value}");
            }
            configFile.WriteLine();
        }
    }
}