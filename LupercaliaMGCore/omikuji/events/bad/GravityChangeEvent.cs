using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {
        private static bool isInGravityChangeEvent = false;

        [OmikujiFunc("Gravity change event", OmikujiType.EVENT_BAD)]
        public static void gravityChangeEvent(CCSPlayerController client) {
            LupercaliaMGCore.getInstance().Logger.LogDebug("Player drew a omikuji and invoked Gravity change event");

            // TODO() Get random range from ConVar
            int randomGravity = random.Next(0, 800);

            string msg;
            if(isInGravityChangeEvent) {
                msg = $"{Omikuji.CHAT_PREFIX} {client.PlayerName} have drew a bad luck! But luckily, Another event ongoing! so nothing happened!";
            } else {
                msg= $"{Omikuji.CHAT_PREFIX} {client.PlayerName} have drew a bad luck! sv_gravity changed to {randomGravity}";
            }

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat(msg);
            }

            if(isInGravityChangeEvent)
                return;

            isInGravityChangeEvent = true;
            ConVar? sv_gravity = ConVar.Find("sv_gravity");

            float oldGravity = sv_gravity!.GetPrimitiveValue<float>();

            sv_gravity.SetValue((float)randomGravity);

            // TODO() Get gravity restore time from ConVar
            float TIMER_INTERVAL_PLACE_HOLDER = 10.0F;

            LupercaliaMGCore.getInstance().AddTimer(TIMER_INTERVAL_PLACE_HOLDER, () => {
                sv_gravity.SetValue(oldGravity);
                foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                    if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                        continue;

                    cl.PrintToChat($"{Omikuji.CHAT_PREFIX} Gravity restored to {oldGravity}!");
                    isInGravityChangeEvent = false;
                }
            });
        }
    }
}