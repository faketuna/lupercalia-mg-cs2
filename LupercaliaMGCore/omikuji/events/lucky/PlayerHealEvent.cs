using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {

        [OmikujiFunc("Player Heal Event", OmikujiType.EVENT_LUCKY)]
        public static void playerHealEvent(CCSPlayerController client) {
            LupercaliaMGCore.getInstance().Logger.LogDebug("Player drew a omikuji and invoked Player heal event.");

            string msg;
            if(client.PawnIsAlive) {
                msg = $"{Omikuji.CHAT_PREFIX} {client.PlayerName} have drew the fortune! {client.PlayerName}'s HP have healed the {PluginSettings.getInstance.m_CVOmikujiEventPlayerHeal.Value}HP!";
            } else {
                msg = $"{Omikuji.CHAT_PREFIX} {client.PlayerName} have drew the fortune! But how unfortunate we can't heal the HP because {client.PlayerName} is already dead.";
            }


            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                cl.PrintToChat(msg);
            }

            if(!client.PawnIsAlive)
                return;

            CCSPlayerPawn playerPawn = client.PlayerPawn.Value!;

            if(playerPawn.MaxHealth < playerPawn.Health + PluginSettings.getInstance.m_CVOmikujiEventPlayerHeal.Value) {
                playerPawn.Health = playerPawn.MaxHealth;
            } else {
                playerPawn.Health += PluginSettings.getInstance.m_CVOmikujiEventPlayerHeal.Value;
            }
            Utilities.SetStateChanged(playerPawn, "CBaseEntity", "m_iHealth");
        }
    }
}