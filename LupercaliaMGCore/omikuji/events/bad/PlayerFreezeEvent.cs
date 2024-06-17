using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {        
        
        [OmikujiFunc("Player Freeze Event", OmikujiType.EVENT_BAD, OmikujiCanInvokeWhen.PLAYER_ALIVE)]
        public static void playerFreezeEvent(CCSPlayerController client) {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Player freeze event");

            if(!client.PawnIsAlive) {
                SimpleLogging.LogDebug("Player freeze event failed due to player is alive. But this is should not be happened.");
                return;
            }

            CCSPlayerPawn? playerPawn = client.PlayerPawn.Value;

            if(playerPawn == null) {
                SimpleLogging.LogDebug("Player freeze event failed due to playerPawn is null.");
                return;
            }
            

            playerPawn.MoveType = MoveType_t.MOVETYPE_OBSOLETE;
            playerPawn.ActualMoveType = MoveType_t.MOVETYPE_OBSOLETE;
            SimpleLogging.LogDebug("Player freeze event: Move type changed to MOVETYPE_OBSOLETE");


            LupercaliaMGCore.getInstance().AddTimer(PluginSettings.getInstance.m_CVOmikujiEventPlayerFreeze.Value, () => {
                playerPawn.MoveType = MoveType_t.MOVETYPE_WALK;
                playerPawn.ActualMoveType = MoveType_t.MOVETYPE_WALK;
                SimpleLogging.LogDebug("Player freeze event: Move type changed to MOVETYPE_WALK");
            });

        }
    }
}