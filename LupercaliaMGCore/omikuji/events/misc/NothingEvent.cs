using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class NothingEvent: OmikujiEvent {
        public string eventName => "Nothing Event";

        public OmikujiType omikujiType => OmikujiType.EVENT_MISC;

        public OmikujiCanInvokeWhen omikujiCanInvokeWhen => OmikujiCanInvokeWhen.ANYTIME;

        public void execute(CCSPlayerController client)
        {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Nothing event");
            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {Omikuji.GetOmikujiLuckMessage(omikujiType, client)} {LupercaliaMGCore.getInstance().Localizer["Omikuji.MiscEvent.NothingEvent.Notification.ButNothingHappened"]}");
            }
        }

        public void initialize() {}

        public double getOmikujiWeight() {
            return PluginSettings.getInstance.m_CVOmikujiEventNothingSelectionWeight.Value;
        }
    }
}