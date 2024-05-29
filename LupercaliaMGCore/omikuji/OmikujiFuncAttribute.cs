using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class OmikujiFuncAttribute: System.Attribute {

        public readonly string name;
        public readonly OmikujiType omikujiType;
        public readonly OmikujiCanInvokeWhen whenOmikujiCanInvoke;

        public OmikujiFuncAttribute(string name, OmikujiType type, OmikujiCanInvokeWhen whenOmikujiCanInvoke = OmikujiCanInvokeWhen.ANYTIME) {
            this.name = name;
            omikujiType = type;
            this.whenOmikujiCanInvoke = whenOmikujiCanInvoke;
        }

    }
}