using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class PlayerRespawnAllEvent: OmikujiEvent {
        public string eventName => "Player Respawn All Event";

        public OmikujiType omikujiType => OmikujiType.EVENT_LUCKY;

        public OmikujiCanInvokeWhen omikujiCanInvokeWhen => OmikujiCanInvokeWhen.ANYTIME;

        public void execute(CCSPlayerController client)
        {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked All player respawn event.");

            CCSPlayerController? alivePlayer = null;

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                if(client.PlayerPawn.Value != null && client.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE) {
                    alivePlayer = cl;
                    break;
                }
            }

            if(alivePlayer == null) {
                SimpleLogging.LogDebug("All player respawn event failed due to no one player is alive.");
                foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                    if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                        continue;

                    cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {Omikuji.GetOmikujiLuckMessage(omikujiType, client)} {LupercaliaMGCore.getInstance().Localizer["Omikuji.LuckyEvent.PlayerRespawnAllEvent.Notification.NoOneAlive"]}");
                }
                return;
            }

            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {Omikuji.GetOmikujiLuckMessage(omikujiType, client)} {LupercaliaMGCore.getInstance().Localizer["Omikuji.LuckyEvent.PlayerRespawnAllEvent.Notification.Respawn"]}");

                if(client.PlayerPawn.Value != null && client.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE)
                    continue;

                cl.Respawn();
                cl.Teleport(alivePlayer.PlayerPawn.Value!.AbsOrigin, alivePlayer.PlayerPawn.Value.AbsRotation);
            }
        }

        public void initialize() {}
        public double getOmikujiWeight() {
            return PluginSettings.getInstance.m_CVOmikujiEventAllPlayerRespawnSelectionWeight.Value;
        }
    }
}