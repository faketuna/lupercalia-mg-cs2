using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using System.Drawing;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class TeamBasedBodyColor
    {
        private LupercaliaMGCore m_CSSPlugin;

        public TeamBasedBodyColor(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
        }

        public void initialize() {
            m_CSSPlugin.RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        }


        private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info) {

            CCSPlayerController player = @event.Userid;

            if(player.Team == CsTeam.None || player.Team == CsTeam.Spectator)
                return HookResult.Continue;

            Color color = Color.Black;
            if(player.Team == CsTeam.CounterTerrorist) {
                color = Color.FromArgb(0, 0 , 255);
            }
            else if(player.Team == CsTeam.Terrorist) {
                color = Color.FromArgb(255, 0 , 0);
            }
            
            CBasePlayerPawn playerPawn = player.PlayerPawn.Value!;

            playerPawn.RenderMode = RenderMode_t.kRenderTransColor;
            playerPawn.Render = color;

            Utilities.SetStateChanged(playerPawn, "CBaseModelEntity", "m_clrRender");
            return HookResult.Continue;
        }
    }
}