using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class OmikujiInfo {

        public readonly OmikujiType omikujiType;
        public readonly BasicOmikujiEvent function;
        public readonly OmikujiCanInvokeWhen whenOmikujiCanInvoke;

        public delegate void BasicOmikujiEvent(CCSPlayerController client);

        public OmikujiInfo(OmikujiType omikujiType, OmikujiCanInvokeWhen whenOmikujiCanInvoke,BasicOmikujiEvent eventFunction) {
            this.omikujiType = omikujiType;
            function = eventFunction;
            this.whenOmikujiCanInvoke = whenOmikujiCanInvoke;
        }
    }


    public enum OmikujiType{
        EVENT_BAD,
        EVENT_LUCKY,
        EVENT_MISC,
    }

    public enum OmikujiCanInvokeWhen {
        PLAYER_DIED,
        PLAYER_ALIVE,
        ANYTIME,
    }
}