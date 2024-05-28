using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class OmikujiFuncAttribute: System.Attribute {

        public readonly string name;
        public readonly OmikujiType omikujiType;

        public OmikujiFuncAttribute(string name, OmikujiType type) {
            this.name = name;
            omikujiType = type;
        }

    }
}