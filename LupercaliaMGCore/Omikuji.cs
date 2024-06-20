using System.Reflection;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using Microsoft.Extensions.Logging;

namespace LupercaliaMGCore {
    public class Omikuji {
        private LupercaliaMGCore m_CSSPlugin;
        public static readonly string CHAT_PREFIX = $" {ChatColors.Gold}[Omikuji]{ChatColors.Default}";

        private static Random random = new Random();

        private List<(OmikujiType omikujiType, double weight)> omikujiTypes = new List<(OmikujiType omikujiType, double weight)>();

        private Dictionary<CCSPlayerController, double> lastCommandUseTime = new Dictionary<CCSPlayerController, double>();
        private Dictionary<CCSPlayerController, bool> isWaitingForEventExecution = new Dictionary<CCSPlayerController, bool>();

        public Omikuji(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.AddCommand("css_omikuji", "draw a fortune.", CommandOmikuji);

            omikujiTypes.Add((OmikujiType.EVENT_BAD, PluginSettings.getInstance.m_CVOmikujiEventWeightBad.Value));
            omikujiTypes.Add((OmikujiType.EVENT_LUCKY, PluginSettings.getInstance.m_CVOmikujiEventWeightLucky.Value));
            omikujiTypes.Add((OmikujiType.EVENT_MISC, PluginSettings.getInstance.m_CVOmikujiEventWeightMisc.Value));

            // For hot reload
            m_CSSPlugin.AddTimer(0.1F, () => {
                SimpleLogging.LogDebug("Late initialization for hot reloading omikuji.");
                foreach(CCSPlayerController client in Utilities.GetPlayers()) {
                    if(!client.IsValid || client.IsBot || client.IsHLTV)
                        continue;
                    
                    lastCommandUseTime[client] = 0.0D;
                }
            });

            OmikujiEvents.initializeOmikujiEvents();
        }

        private HookResult OnPlayerConnected(EventPlayerConnect @event, GameEventInfo info) {
            CCSPlayerController? client = @event.Userid;

            if(client == null)
                return HookResult.Continue;

            lastCommandUseTime[client] = 0.0D;
            isWaitingForEventExecution[client] = false;
            return HookResult.Continue;
        }

        private void CommandOmikuji(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;
            
            if(isWaitingForEventExecution[client]) {
                client.PrintToChat($"{CHAT_PREFIX} Your omikuji is not yet to be executed! Please wait!");
                return;
            }

            if(Server.EngineTime - lastCommandUseTime[client] < PluginSettings.getInstance.m_CVOmikujiCommandCooldown.Value) {
                client.PrintToChat($"{CHAT_PREFIX} Your omikuji is in cooldown! Please wait for {(Server.EngineTime - lastCommandUseTime[client]).ToString("#.#")} seconds.");
                return;
            }

            SimpleLogging.LogDebug($"[Omikuji] [Player {client.PlayerName}] trying to draw omikuji.");
            SimpleLogging.LogTrace($"[Omikuji] [Player {client.PlayerName}] Picking random omikuji type.");
            OmikujiType randomOmikujiType = getRandomOmikujiType();
            var events = OmikujiEvents.getEvents()[randomOmikujiType];
            bool isPlayerAlive = client.PawnIsAlive;

            OmikujiEvent omikuji;
            
            SimpleLogging.LogTrace($"[Omikuji] [Player {client.PlayerName}] Picking random omikuji.");
            while(true) {
                omikuji = selectWeightedRandom(events);

                if(omikuji.omikujiCanInvokeWhen == OmikujiCanInvokeWhen.ANYTIME) {
                    break;
                }
                else if (omikuji.omikujiCanInvokeWhen == OmikujiCanInvokeWhen.PLAYER_DIED && !isPlayerAlive) {
                    break;
                }
                else if (omikuji.omikujiCanInvokeWhen == OmikujiCanInvokeWhen.PLAYER_ALIVE && isPlayerAlive) {
                    break;
                }
            }

            isWaitingForEventExecution[client] = true;
            m_CSSPlugin.AddTimer(random.Next(PluginSettings.getInstance.m_CVOmikujiCommandExecutionDelayMin.Value, PluginSettings.getInstance.m_CVOmikujiCommandExecutionDelayMax.Value), () => {
                SimpleLogging.LogTrace($"[Omikuji] [Player {client.PlayerName}] Executing omikuji...");
                lastCommandUseTime[client] = Server.EngineTime;
                isWaitingForEventExecution[client] = false;
                omikuji.execute(client);
            });
        }

        private OmikujiType getRandomOmikujiType() {
            return selectWeightedRandom(omikujiTypes);
        }


        private static T selectWeightedRandom<T>(List<(T item, double weight)> weightedItems) {
            double totalWeight = 0.0D;
            foreach(var item in weightedItems) {
                totalWeight += item.weight;
            }

            double randomVal = random.NextDouble() * totalWeight;

            foreach(var item in weightedItems) {
                if(randomVal < item.weight) {
                    return item.item;
                }

                randomVal -= item.weight;
            }

            return weightedItems[0].item;
        }

        private static OmikujiEvent selectWeightedRandom(List<OmikujiEvent> weightedItems) {
            double totalWeight = 0.0D;
            foreach(var item in weightedItems) {
                totalWeight += item.getOmikujiWeight();
            }

            double randomVal = random.NextDouble() * totalWeight;

            foreach(var item in weightedItems) {
                if(randomVal < item.getOmikujiWeight()) {
                    return item;
                }

                randomVal -= item.getOmikujiWeight();
            }

            return weightedItems[0];
        }
    }
}