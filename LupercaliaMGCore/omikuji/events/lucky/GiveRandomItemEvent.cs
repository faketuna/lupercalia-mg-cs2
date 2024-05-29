using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public static partial class OmikujiEvents {

        [OmikujiFunc("Give Random Item Event", OmikujiType.EVENT_LUCKY, OmikujiCanInvokeWhen.PLAYER_ALIVE)]
        public static void giveRandomItemEvent(CCSPlayerController client) {
            LupercaliaMGCore.getInstance().Logger.LogDebug("Player drew a omikuji and invoked Give random item event");

            string randomItem;
            foreach(CCSPlayerController cl in Utilities.GetPlayers()) {
                if(!cl.IsValid || cl.IsBot || cl.IsHLTV)
                    continue;

                randomItem = pickRandomItem();

                cl.GiveNamedItem((CsItem)Enum.Parse(typeof(CsItem), randomItem));

                cl.PrintToChat($"{Omikuji.CHAT_PREFIX} {client.PlayerName} have drew the fortune! And you have received the {randomItem}!");
            }
        }


        private static List<CsItem> invalidItems = new List<CsItem>() {
                CsItem.XRayGrenade,
                CsItem.IncGrenade,
                CsItem.FragGrenade,
                CsItem.HE,
                CsItem.Taser,
                CsItem.Knife,
                CsItem.DefaultKnifeCT,
                CsItem.DefaultKnifeT,
                CsItem.Revolver,
                CsItem.P2K,
                CsItem.CZ,
                CsItem.AutoSniperCT,
                CsItem.AutoSniperT,
                CsItem.Diversion,
                CsItem.KevlarHelmet,
                CsItem.Dualies,
                CsItem.Firebomb,
                CsItem.Glock18,
                CsItem.Krieg,
                // DZ Items are not implemented yet
                CsItem.Bumpmine,
                CsItem.BreachCharge,
                CsItem.Shield,
                CsItem.Bomb,
                CsItem.Tablet,
            };


        // HE Grenade giving rate is definitely low. investigate later.
        private static string pickRandomItem() {
            string item;
            string[] items = Enum.GetNames(typeof(CsItem));
            int itemsCount = items.Count();

            int randomNum;

            Random random = new Random();
            while(true) {
                randomNum = random.Next(0, itemsCount);

                item = items[randomNum];
                if(!invalidItems.Contains((CsItem)Enum.Parse(typeof(CsItem), item))) {
                    break;
                }
            }

            return item;
        }
    }
}