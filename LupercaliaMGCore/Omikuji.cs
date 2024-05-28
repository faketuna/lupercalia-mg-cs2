using System.Reflection;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace LupercaliaMGCore {
    public class Omikuji {
        private LupercaliaMGCore m_CSSPlugin;
        public static readonly string CHAT_PREFIX = $" {ChatColors.Gold}[Omikuji]{ChatColors.Default}";


        private List<OmikujiInfo> events;

        public Omikuji(LupercaliaMGCore plugin) {
            m_CSSPlugin = plugin;

            m_CSSPlugin.AddCommand("css_omikuji", "draw a fortune.", CommandOmikuji);
            events = findAllOmikujiMethods(typeof(OmikujiEvents));
        }


        private void CommandOmikuji(CCSPlayerController? client, CommandInfo info) {
            if(client == null)
                return;

            Random rand = new Random();

            int randomValue = rand.Next(0, events.Count);

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

                omikujiInfos.Add(new OmikujiInfo(attribute.omikujiType, delegateFunc));
            }

            return omikujiInfos;
        }
    }
}