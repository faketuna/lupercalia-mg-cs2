using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {

        [OmikujiFunc("Player Respawn Event", OmikujiType.EVENT_LUCKY, OmikujiCanInvokeWhen.PLAYER_DIED)]
        public static void playerRespawnEvent(CCSPlayerController client) {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Player respawn event");

            string msg;
            if (!client.PawnIsAlive) {
                msg = $"{Omikuji.CHAT_PREFIX} Lucky! {client.PlayerName} have re-spawned!";
            } else {
                msg = $"{Omikuji.CHAT_PREFIX} Lucky! {client.PlayerName} have draw a fortune! But how unfortunate nothing happened because {client.PlayerName} are still alive.";
            }

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;
                
                if(!client.PawnIsAlive && cl.PawnIsAlive) {
                    client.Respawn();
                    client.Teleport(cl.PlayerPawn!.Value!.AbsOrigin);
                }

                cl.PrintToChat(msg);
            }
        }
    }
}