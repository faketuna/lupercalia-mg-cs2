using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using System.Drawing;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Cvars;

namespace LupercaliaMGCore {
    public class TeamBasedBodyColor
    {
        private LupercaliaMGCore m_CSSPlugin;

        public TeamBasedBodyColor(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
            
            m_CSSPlugin.RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        }

        private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info) {
            CCSPlayerController? player = @event.Userid;

            if(player == null || !player.IsValid)
                return HookResult.Continue;

            if(player.Team == CsTeam.None || player.Team == CsTeam.Spectator)
                return HookResult.Continue;

            SimpleLogging.LogDebug($"[Team Based Body Color] [Player {player.PlayerName}] spawned");

            Color color = Color.Black;
            List<int> rgb = new List<int>();
            if(player.Team == CsTeam.CounterTerrorist) {
                rgb = PluginSettings.getInstance.m_CVTeamColorCT.Value.Split(',').Select(s => int.Parse(s.Trim())).ToList();
            }
            else if(player.Team == CsTeam.Terrorist) {
                rgb = PluginSettings.getInstance.m_CVTeamColorT.Value.Split(',').Select(s => int.Parse(s.Trim())).ToList();
            }
            color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
            SimpleLogging.LogTrace($"[Team Based Body Color] Player {player.PlayerName}'s team: {player.Team}, color: {color.R} {color.G} {color.B}");
            
            CBasePlayerPawn playerPawn = player.PlayerPawn.Value!;

            playerPawn.RenderMode = RenderMode_t.kRenderTransColor;
            playerPawn.Render = color;
            SimpleLogging.LogTrace($"[Team Based Body Color] [Player {player.PlayerName}] render mode changed to RenderMode_t.kRenderTransColor");

            SimpleLogging.LogTrace($"[Team Based Body Color] [Player {player.PlayerName}] Sending state change.");
            Utilities.SetStateChanged(playerPawn, "CBaseModelEntity", "m_clrRender");
            SimpleLogging.LogDebug($"[Team Based Body Color] [Player {player.PlayerName}] Done.");
            return HookResult.Continue;
        }
    }
}