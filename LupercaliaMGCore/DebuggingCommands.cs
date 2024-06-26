using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Admin;
using Vector = CounterStrikeSharp.API.Modules.Utils.Vector;

namespace LupercaliaMGCore {
    public class Debugging {
        private LupercaliaMGCore m_CSSPlugin;

        private Dictionary<CCSPlayerController, Vector> savedPlayerPos = new();


        public Debugging(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;
            m_CSSPlugin.AddCommand("css_dbg_savepos", "Save current location for teleport", CommandSavePos);
            m_CSSPlugin.AddCommand("css_dbg_restorepos", "Use saved location to teleport", CommandRestorePos);
        }

        private void CommandSavePos(CCSPlayerController? client, CommandInfo info) {
            if(client == null) 
                return;
            
            if(!PluginSettings.getInstance.m_CVDebuggingEnabled.Value) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Debugging feature is disabled."));
                return;
            }

            if(!client.PawnIsAlive) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You should be alive to use this command."));
                return;
            }

            var playerPawn = client.PlayerPawn.Value;

            if(playerPawn == null)
                return;

            var clientAbsPos = playerPawn.AbsOrigin;

            if(clientAbsPos == null) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Failed to retrieve your position!"));
                return;
            }

            var vector = new Vector(clientAbsPos.X, clientAbsPos.Y, clientAbsPos.Z);

            savedPlayerPos[client] = vector;

            client.PrintToChat(LupercaliaMGCore.MessageWithPrefix($"Location saved! {vector.X}, {vector.Y}, {vector.Z}"));
        }

        private void CommandRestorePos(CCSPlayerController? client, CommandInfo info) {
            if(client == null) 
                return;
            
            if(!PluginSettings.getInstance.m_CVDebuggingEnabled.Value) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("Debugging feature is disabled."));
                return;
            }

            if(!client.PawnIsAlive) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("You should be alive to use this command."));
                return;
            }

            var playerPawn = client.PlayerPawn.Value;

            if(playerPawn == null)
                return;

            Vector? vector = null;

            if(!savedPlayerPos.TryGetValue(client, out vector) || vector == null) {
                client.PrintToChat(LupercaliaMGCore.MessageWithPrefix("There is no saved location! save location first!"));
                return;
            }

            client.PrintToChat(LupercaliaMGCore.MessageWithPrefix($"Teleported to {vector.X}, {vector.Y}, {vector.Z}"));
            playerPawn.Teleport(vector, playerPawn.AbsRotation);
        }
    }
}