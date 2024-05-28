using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {

        [OmikujiFunc("All Player Respawn Event", OmikujiType.EVENT_LUCKY)]
        public static void PlayerRespawnAllEvent(CCSPlayerController client) {
            LupercaliaMGCore.getInstance().Logger.LogDebug("Player drew a omikuji and invoked All player respawn event.");

            CCSPlayerController? alivePlayer = null;

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                if(cl.PawnIsAlive) {
                    alivePlayer = cl;
                    break;
                }
            }

            if(alivePlayer == null) {
                LupercaliaMGCore.getInstance().Logger.LogDebug("All player respawn event failed due to no one player is alive.");
                foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                    if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                        continue;

                    cl.PrintToChat($"{Omikuji.CHAT_PREFIX} Lucky! {client.PlayerName} have draw a fortune! But how unfortunate nothing happened because no one is alive.");
                }
                return;
            }

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {client.PlayerName} have drew the mega luck! re-spawning the all players!!!");

                if(cl.PawnIsAlive)
                    continue;

                cl.Respawn();
                cl.Teleport(alivePlayer.PlayerPawn.Value!.AbsOrigin, alivePlayer.PlayerPawn.Value.AbsRotation);
            }
        }
    }
}