using System.Drawing;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {
        [OmikujiFunc("Chicken Spawn Event", OmikujiType.EVENT_MISC)]
        public static void chickenSpawnEvent(CCSPlayerController client) {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Chicken spawn event");
            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;


                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} (・∋・)コケ、コケコッコー");

                if(cl.PawnIsAlive)
                    createGamingChicken(client);
            }
        }

        private static void createGamingChicken(CCSPlayerController client) {
            CChicken? ent = Utilities.CreateEntityByName<CChicken>("chicken");

            if(ent == null)
                return;
            
            ent.DispatchSpawn();
            ent.Teleport(client.PlayerPawn.Value!.AbsOrigin!);
            ent.CBodyComponent!.SceneNode!.GetSkeletonInstance().Scale = PluginSettings.getInstance.m_CVOmikujiEventChickenBodyScale.Value;

            double hue = 0.0;
            LupercaliaMGCore.getInstance().AddTimer(0.01f, () => {
                if(!ent.IsValid)
                    return;

                if(hue >= 360.0)
                    hue = 0.0;
                
                ent.RenderMode = RenderMode_t.kRenderTransColor;
                ent.Render = ColorFromHSV(hue, 1, 1);
                Utilities.SetStateChanged(ent, "CBaseModelEntity", "m_clrRender");
                hue += 30.0F;
            }, CounterStrikeSharp.API.Modules.Timers.TimerFlags.REPEAT);

            LupercaliaMGCore.getInstance().AddTimer(PluginSettings.getInstance.m_CVOmikujiEventChickenTime.Value, () => {
                if(ent.IsValid)
                    ent.AcceptInput("Break");
            });
        }

        public static Color ColorFromHSV(double hue, double saturation, double brightness) {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            brightness = brightness * 255;
            int v = Convert.ToInt32(brightness);
            int p = Convert.ToInt32(brightness * (1 - saturation));
            int q = Convert.ToInt32(brightness * (1 - f * saturation));
            int t = Convert.ToInt32(brightness * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

    }
}