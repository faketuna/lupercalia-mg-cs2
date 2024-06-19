using CounterStrikeSharp.API.Core;

namespace LupercaliaMGCore {
    public interface OmikujiEvent {

        string eventName {get;}

        OmikujiType omikujiType {get;}

        OmikujiCanInvokeWhen omikujiCanInvokeWhen {get;}

        void execute(CCSPlayerController client);

        void initialize();

        double getOmikujiWeight();
    }
}