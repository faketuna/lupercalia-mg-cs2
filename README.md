# Lupercalia MG server core plugin

CounterStrikeSharp plugin for Lupercalia MG Server(Soon™)

# Features


- [ ] General
  - [x] DuckFix
  - [x] Map config
  - [ ] Rocket
  - [ ] Easy spectate (TODO!) 
  - [x] Vote map restart
  - [x] Vote round restart
  - [x] Scheduled shutdown
- [x] Multigames
  - [x] Team Based Body Color
  - [x] Team Scramble
  - [x] Anti camp system (TODO!)
  - [x] Round end damage immunity 
  - [x] Round end weapon strip
- [x] Course
  - [x] Auto Respawn with spawn killing detection
- [x] Fun
  - [x] Omikuji

## General

### DuckFix

This feature removes duck cooldown when spamming `+duck` button.

### Map config

- Support full map name matching. i.e. `mg_test_multigames`
- Support start with any prefix. i.e. `mg_`

#### Usage

Put `***.cfg` to `csgo/cfg/lupercalia/map/` folder

This map config folder will automatically created when not exists.

#### ConVar

In default `lp_mg_mapcfg_type` is 0, which means disabled.

If you want to use exact mach i.e. `de_dust2.cfg` set `lp_mg_mapcfg_type` to 1

Or you want to use forward match i.e. `de_.cfg` set `lp_mg_mapcfg_type` to 2

---

In default `lp_mg_mapcfg_execution_timing` is 1 which means Execute on map start only.

If you want to execute at only round start set `lp_mg_mapcfg_execution_timing` to 2

Or you want to execute at both set `lp_mg_mapcfg_execution_timing` to 3


### Rocket

Probability based vertical launching system. And high chance(default) to die due to rocket accident.

Same feature as [Rocket](https://github.com/faketuna/sm-csgo-rocket)

### Easy spectate

~~Will be implemented after Trace Ray feature implemented in CounterStrikeSharp.~~ I found a way to get the player looking entity.

But another problem found. the `spec_player` command is not works as like CS:GO. it means player is actually spec a player when use `spec_player` command but player not spectate the target's POV when a freelook state.

Spectate aimed player when player is spectating/died and pressing `+use`

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

### Anti camp system

- When camp detected player will be glow.

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

See generated configs in `csgo/cfg/lupercalia/`