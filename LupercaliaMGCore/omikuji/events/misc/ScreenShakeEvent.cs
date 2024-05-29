using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {

        [OmikujiFunc("Screen Shake Event", OmikujiType.EVENT_MISC)]
        public static void screenShakeEvent(CCSPlayerController client) {
            LupercaliaMGCore.getInstance().Logger.LogDebug("Player drew a omikuji and invoked Screen shake event");

            CEnvShake? shakeEnt = Utilities.CreateEntityByName<CEnvShake>("env_shake");
            if(shakeEnt == null) {
                LupercaliaMGCore.getInstance().Logger.LogError("Failed to create env_shake!");
                return;
            }

            shakeEnt.Spawnflags = 5U;
            shakeEnt.Amplitude = PluginSettings.getInstance.m_CVOmikujiEventScreenShakeAmplitude.Value;
            shakeEnt.Duration = PluginSettings.getInstance.m_CVOmikujiEventScreenShakeDuration.Value;
            shakeEnt.Frequency = PluginSettings.getInstance.m_CVOmikujiEventScreenShakeFrequency.Value;

            shakeEnt.DispatchSpawn();

            shakeEnt.AcceptInput("StartShake");

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {client.PlayerName} erupted the volcano! Be prepared for impact!!!");
            }

            shakeEnt.Remove();
        }
    }
}