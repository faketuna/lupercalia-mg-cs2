using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class PlayerHealEvent: OmikujiEvent {
        public string eventName => "Player Heal Event";

        public OmikujiType omikujiType => OmikujiType.EVENT_LUCKY;

        public OmikujiCanInvokeWhen omikujiCanInvokeWhen => OmikujiCanInvokeWhen.PLAYER_ALIVE;

        public void execute(CCSPlayerController client)
        {
            SimpleLogging.LogDebug("Player drew a omikuji and invoked Player heal event.");

            string msg;

            bool isPlayerAlive = client.PlayerPawn.Value != null && client.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE;

            if(isPlayerAlive) {
                msg = $"{Omikuji.CHAT_PREFIX} {Omikuji.GetOmikujiLuckMessage(omikujiType, client)} {LupercaliaMGCore.getInstance().Localizer["Omikuji.LuckyEvent.PlayerHealEvent.Notification.Healed", client.PlayerName, PluginSettings.getInstance.m_CVOmikujiEventPlayerHeal.Value]}";
            } else {
                msg = $"{Omikuji.CHAT_PREFIX} {Omikuji.GetOmikujiLuckMessage(omikujiType, client)} {LupercaliaMGCore.getInstance().Localizer["Omikuji.LuckyEvent.PlayerHealEvent.Notification.PlayerIsDead", client.PlayerName]}";
            }


            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat(msg);
            }

            if(!isPlayerAlive)
                return;

            CCSPlayerPawn playerPawn = client.PlayerPawn.Value!;

            if(playerPawn.MaxHealth < playerPawn.Health + PluginSettings.getInstance.m_CVOmikujiEventPlayerHeal.Value) {
                playerPawn.Health = playerPawn.MaxHealth;
            } else {
                playerPawn.Health += PluginSettings.getInstance.m_CVOmikujiEventPlayerHeal.Value;
            }
            Utilities.SetStateChanged(playerPawn, "CBaseEntity", "m_iHealth");
        }

        public void initialize() {}

        public double getOmikujiWeight() {
            return PluginSettings.getInstance.m_CVOmikujiEventPlayerHealSelectionWeight.Value;
        }
    }
}