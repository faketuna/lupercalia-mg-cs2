using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Entities.Constants;

namespace LupercaliaMGCore {
    public class MiscCommands {
        private LupercaliaMGCore m_CSSPlugin;

        public MiscCommands(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.AddCommand("css_knife", "give knife", CommandGiveKnife);
        }


        private void CommandGiveKnife(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;
            
            if(!client.PawnIsAlive) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You should be alive to use this command."));
                return;
            }

            if(!PluginSettings.getInstance.m_CVMiscCMDGiveKnifeEnabled.Value) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("This feature is disabled."));
                return;
            }

            CCSPlayerPawn? playerPawn = client.PlayerPawn.Value;

            if(playerPawn == null) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("This command is not usable currently."));
                return;
            }

            CPlayer_WeaponServices? weaponServices = playerPawn.WeaponServices;

            if(weaponServices == null) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("This command is not usable currently."));
                return;
            }

            bool found = false;
            foreach(var weapon in weaponServices.MyWeapons) {
                if(weapon.Value?.DesignerName == "weapon_knife") {
                    found = true;
                    break;
                }
            }

            if(found) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You already have knife!"));
            }
            else {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You retrieved the knife!"));
                client.GiveNamedItem(CsItem.Knife);
            }
        }
    }
}