using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Freecam2
{
    public class Main : BaseScript
    {
        bool IsInFreecam = false;

        public Main()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(ResourceStart);
        }

        private async void ResourceStart(string Name)
        {
            if (GetCurrentResourceName() != Name) return;

            RegisterCommand("freecam", new Action<int, List<object>, string>((source, args, raw) =>
            {
                IsInFreecam = !IsInFreecam;
                if (IsInFreecam)
                {
                    TriggerEvent("mosh_notify:notify", "INFO", "<span class=\"text-black\">You are now using Freecam²! Type /freecam again to disable Freecam!</span>", "info", "info", 5000);
                    SendBasicMessage("You are now using Freecam²! Type /freecam again to disable Freecam!");
                    Freecam.Enable();
                }
                else
                    Freecam.Disable();
            }), false);
        }

        public static void SendBasicMessage(string message)
        {
            TriggerEvent("chat:addMessage", new
            {
                color = new[] { 236, 80, 156 },
                args = new[] { "[Freecam²]", message }
            });
        }
    }
}
