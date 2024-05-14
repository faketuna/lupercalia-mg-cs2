# Lupercalia MG server core plugin

CounterStrikeSharp plugin for Lupercalia MG Server(Soon™)

# Features


- [ ] General
  - [x] DuckFix
  - [ ] Map config
  - [ ] Rocket
  - [ ] Easy spectate 
  - [ ] Vote map restart
  - [ ] Vote round restart
  - [ ] Scheduled shutdown
- [ ] Multigames
  - [x] Team Based Body Color
  - [x] Team Scramble
  - [ ] Anti camp system
  - [ ] Round end damage immunity 
  - [ ] Round end weapon strip
- [ ] Course
  - [ ] Auto Respawn with spawn killing detection

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

# ConVars / Config

Config file will be generated in `csgo/cfg/lupercalia/`

```
- lp_mg_teamcolor_ct            "0, 0, 255" | Counter Terrorist's Body color. R, G, B
- lp_mg_teamcolor_t             "255, 0, 0" | Terrorist's Body color. R, G, B
- lp_mg_teamscramble_enabled    "1"         | Should team is scrambled after round end
- lp_mg_mapcfg_type             "0"         | Map configuration type. 0: disabled, 1: Exact match, 2: Partial Match, 3: Both
```