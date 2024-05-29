using System.Reflection;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class Omikuji {
        private LupercaliaMGCore m_CSSPlugin;
        public static readonly string CHAT_PREFIX = $" {ChatColors.Gold}[Omikuji]{ChatColors.Default}";

        private Random random = new Random();

        private List<OmikujiInfo> events;

        public Omikuji(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.AddCommand("css_omikuji", "draw a fortune.", CommandOmikuji);
            events = findAllOmikujiMethods(typeof(OmikujiEvents));
        }


        private void CommandOmikuji(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;

            int randomValue;
            OmikujiType randomOmikujiType = getRandomOmikujiType();
            bool isPlayerAlive = client.PawnIsAlive;
            OmikujiInfo omikujiInfo;

            while(true) {
                randomValue = random.Next(0, events.Count);
                if(events[randomValue].omikujiType != randomOmikujiType) {
                    continue;
                }

                omikujiInfo = events[randomValue];

                if(omikujiInfo.whenOmikujiCanInvoke == OmikujiCanInvokeWhen.ANYTIME)
                    break;
                
                if(omikujiInfo.whenOmikujiCanInvoke == OmikujiCanInvokeWhen.PLAYER_ALIVE && isPlayerAlive)
                    break;
                
                if(omikujiInfo.whenOmikujiCanInvoke == OmikujiCanInvokeWhen.PLAYER_DIED && !isPlayerAlive)
                    break;

            }
            events[randomValue].function.Invoke(client);
        }

        private static List<OmikujiInfo> findAllOmikujiMethods(Type targetType) {
            List<OmikujiInfo> omikujiInfos = new List<OmikujiInfo>();

            MethodInfo[] methods = targetType.GetMethods(BindingFlags.Static | BindingFlags.Public);

            foreach(MethodInfo method in methods) {
                OmikujiFuncAttribute? attribute = method.GetCustomAttribute<OmikujiFuncAttribute>();

                if(attribute == null)
                    continue;
                

                ParameterInfo[] parameters = method.GetParameters();

                if (method.ReturnType != typeof(void) || parameters.Length != 1 || parameters[0].ParameterType != typeof(CCSPlayerController))
                    continue;
                
                OmikujiInfo.BasicOmikujiEvent delegateFunc = (OmikujiInfo.BasicOmikujiEvent)Delegate.CreateDelegate(typeof(OmikujiInfo.BasicOmikujiEvent), method);

                omikujiInfos.Add(new OmikujiInfo(attribute.omikujiType, attribute.whenOmikujiCanInvoke ,delegateFunc));
            }

            return omikujiInfos;
        }

        private OmikujiType getRandomOmikujiType() {
            int weightMisc = PluginSettings.getInstance.m_CVOmikujiEventWeightMisc.Value;
            int weightBad = PluginSettings.getInstance.m_CVOmikujiEventWeightBad.Value;
            int weightLucky = PluginSettings.getInstance.m_CVOmikujiEventWeightLucky.Value;
            int rand = random.Next(0, weightMisc + weightBad + weightLucky);

            OmikujiType type;

            if(rand < weightMisc) {
                type = OmikujiType.EVENT_MISC;
            }
            else if (rand < weightMisc + weightBad) {
                type = OmikujiType.EVENT_BAD;
            }
            else {
                type = OmikujiType.EVENT_LUCKY;
            }

            return type;
        }
    }
}