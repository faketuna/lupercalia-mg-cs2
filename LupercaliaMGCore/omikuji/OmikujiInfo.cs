using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public class OmikujiInfo {

        public readonly OmikujiType omikujiType;
        public readonly BasicOmikujiEvent function;

        public delegate void BasicOmikujiEvent(CCSPlayerController client);

        public OmikujiInfo(OmikujiType omikujiType, BasicOmikujiEvent eventFunction) {
            this.omikujiType = omikujiType;
            function = eventFunction;
        }
    }


    public enum OmikujiType{
        EVENT_BAD,
        EVENT_LUCKY,
        EVENT_MISC,
    }
}