using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class PlayerRespawnEvent: OmikujiEvent {
        public string eventName => "Player Respawn Event";

        public OmikujiType omikujiType => OmikujiType.EVENT_LUCKY;

        public OmikujiCanInvokeWhen omikujiCanInvokeWhen => OmikujiCanInvokeWhen.PLAYER_DIED;

        public void execute(CCSPlayerController client)
        {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Player respawn event");

            string msg;

            bool isPlayerAlive = client.PlayerPawn.Value != null && client.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE;

            if (isPlayerAlive) {
                msg = $"{Omikuji.CHAT_PREFIX} Lucky! {client.PlayerName} have re-spawned!";
            } else {
                msg = $"{Omikuji.CHAT_PREFIX} Lucky! {client.PlayerName} have draw a fortune! But how unfortunate nothing happened because {client.PlayerName} are still alive.";
            }

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;
                
                if((client.PlayerPawn.Value == null || client.PlayerPawn.Value.LifeState != (byte)LifeState_t.LIFE_ALIVE) && 
                    (cl.PlayerPawn.Value != null && cl.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE)) {
                    Server.NextFrame(() => {
                        client.Respawn();
                        client.Teleport(cl.PlayerPawn!.Value!.AbsOrigin);
                    });
                }

                cl.PrintToChat(msg);
            }
        }

        public void initialize() {}

        public double getOmikujiWeight() {
            return PluginSettings.getInstance.m_CVOmikujiEventPlayerRespawnSelectionWeight.Value;
        }
    }
}