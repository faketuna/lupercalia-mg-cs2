using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Commands.Targeting;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class MiscCommands {
        private LupercaliaMGCore m_CSSPlugin;

        public MiscCommands(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.AddCommand("css_knife", "give knife", CommandGiveKnife);
            m_CSSPlugin.AddCommand("css_spec", "Spectate", CommandSpectate);
        }


        private void CommandGiveKnife(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;
            
            if(client.PlayerPawn.Value == null || client.PlayerPawn.Value.LifeState != (byte)LifeState_t.LIFE_ALIVE) {
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

        private void CommandSpectate(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;

            if(info.ArgCount <= 1) {
                if(client.Team == CsTeam.Spectator || client.Team == CsTeam.None)
                    return;


                client.ChangeTeam(CsTeam.Spectator);
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You've moved to spectator!"));
                return;
            }

            if(client.PlayerPawn.Value == null || client.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You can only spectate player while dead or spectator."));
                return;
            }

            TargetResult targets = info.GetArgTargetResult(1);

            if(targets.Count() > 1) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix($"Multiple target found! Please target the one person! ({targets.Count()} found)"));
                return;
            }

            client.PrintToChat(LupercaliaMGCore.MessageWithPrefix($"You are now spectating {targets.First().PlayerName}."));
            client.ExecuteClientCommandFromServer($"spec_player {targets.First().PlayerName}");
        }
    }
}