# Lupercalia MG server core plugin

CounterStrikeSharp plugin for Lupercalia MG Server(Soon™)

# Features


- [ ] General
  - [x] DuckFix
  - [ ] Map config
  - [ ] Rocket
  - [ ] Easy spectate (TODO!) 
  - [x] Vote map restart
  - [x] Vote round restart
  - [x] Scheduled shutdown
- [ ] Multigames
  - [x] Team Based Body Color
  - [x] Team Scramble
  - [ ] Anti camp system
  - [x] Round end damage immunity 
  - [x] Round end weapon strip
- [ ] Course
  - [x] Auto Respawn with spawn killing detection
- [ ] Fun
  - [ ] Omikuji

## General

### DuckFix

This feature removes duck cooldown when spamming `+duck` button.

### Map config

- Support full map name matching. i.e. `mg_test_multigames`
- Support start with any prefix. i.e. `mg_`


### Rocket

Probability based vertical launching system. And high chance(default) to die due to rocket accident.

Same feature as [Rocket](https://github.com/faketuna/sm-csgo-rocket)

### Easy spectate

Will be implemented after Trace Ray feature implemented in CounterStrikeSharp.

Spectate aimed player when player is spectating/died and pressing `+use`

Maybe implemented as separated plugin.

Same feature as [sm-CSGO-easyspectate](https://github.com/faketuna/sm-CSGO-easyspectate). 

### Vote map restart

Rock The Vote style map restart system.

Same feature as [votemaprestart](https://github.com/faketuna/sm-CSGO-votemaprestart)

### Vote round restart

Rock The Vote style round restart system.

Same feature as [voterestart](https://github.com/faketuna/sm-CSGO-voterestart)

### Scheduled shutdown

Shutdown a server in certain time.

Partial feature from [sm-csgo-scheduled-shutdown](https://github.com/faketuna/sm-csgo-scheduled-shutdown)

## Multigames

### Team Based Body Color

Same feature as [TeamBasedBodyColor](https://github.com/faketuna/TeamBasedBodyColor)

### Team Scramble

- Scrambles team when round end.
- More feature is may will implement

### Anti camp system

- When camp detected player will be slapped

### Round end damage immunity

Player grant immunity for damage when round end.

Same feature as Damage Immunity plugin (Forget to upload in GitHub).

### Round end weapon strip

Player's weapon will be removed when round end / or round prestart.

Same feature as [roundEndWeaponStrip](https://github.com/faketuna/roundEndWeaponStrip).

## Course

### Auto Respawn

- Auto respawn player when died
- Repeat kill detection

## Fun

### Omikuji

Probability based event system. When player type `!omikuji` in chat something good/bad/unknown event happens.

# ConVars / Config

Config file will be generated in `csgo/cfg/lupercalia/`

```
- lp_mg_teamcolor_ct                      0, 0, 255   | Counter Terrorist's Body color. R, G, B
- lp_mg_teamcolor_t                       255, 0, 0   | Terrorist's Body color. R, G, B
- lp_mg_teamscramble_enabled              1           | Should team is scrambled after round end
- lp_mg_mapcfg_type                       0           | Map configuration type. 0: disabled, 1: Exact match, 2: Partial Match, 3: Both
- lp_mg_vmr_allowed_time                  60.0        | How long allowed to use vote command after map loaded in seconds.
- lp_mg_vmr_vote_threshold                0.7         | How percent of votes required to initiate the map restart.      
- lp_mg_vmr_restart_time                  10.0        | How long to take restarting map after vote passed.
- lp_mg_vrr_vote_threshold                0.7         | How percent of votes required to initiate the round restart.
- lp_mg_vrr_restart_time                  10.0        | How long to take restarting round after vote passed.
- lp_mg_redi_enabled                      1           | Should player grant damage immunity after round end until next round starts.
- lp_mg_rews_enabled                      1           | Should player's weapons are removed before new round starts.
- lp_mg_scheduled_shutdown_time           0500        | Server will be shutdown in specified time. Format is HHmm
- lp_mg_scheduled_shutdown_warn_time      10          | Show shutdown warning countdown if lp_mg_scheduled_shutdown_round_end is false.
- lp_mg_scheduled_shutdown_round_end      1           | When set to true server will be shutdown after round end.
- lp_mg_auto_respawn_enabled              0           | Auto respawn feature is enabled.
- lp_mg_auto_respawn_repeat_kill_time     1.0         | Seconds to detect as spawn killing.
- lp_mg_auto_respawn_time                 1.0         | How long to respawn after death.
- lp_mg_anti_camp_enabled                 1           | Anti camp enabled
- lp_mg_anti_camp_detection_time          10.0        | How long to detect as camping in seconds.
- lp_mg_anti_camp_detection_radius        400.0       | Range of area for player should move for avoiding the detected as camping.
- lp_mg_anti_camp_detection_interval      0.1         | Interval to run camping check in seconds.
- lp_mg_anti_camp_glowing_time            10.0        | How long to detected player model are keep glowing in seconds.
```