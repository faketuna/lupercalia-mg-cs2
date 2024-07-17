using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class ScreenShakeEvent: OmikujiEvent {
        public string eventName => "Screen Shake Event";

        public OmikujiType omikujiType => OmikujiType.EVENT_MISC;

        public OmikujiCanInvokeWhen omikujiCanInvokeWhen => OmikujiCanInvokeWhen.ANYTIME;

        public void execute(CCSPlayerController client)
        {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Screen shake event");

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

                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {LupercaliaMGCore.getInstance().Localizer["Omikuji.MiscEvent.ScreenShakeEvent.Notification.PrepareForImpact", client.PlayerName]}");
            }

            shakeEnt.Remove();
        }

        public void initialize() {}

        public double getOmikujiWeight() {
            return PluginSettings.getInstance.m_CVOmikujiEventScreenShakeSelectionWeight.Value;
        }
    }
}