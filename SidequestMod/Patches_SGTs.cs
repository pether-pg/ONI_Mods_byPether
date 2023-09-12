using Database;
using HarmonyLib;
using JetBrains.Annotations;
using Klei.AI;
using Klei.CustomSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using static STRINGS.UI.FRONTEND;

namespace SidequestMod
{
    class Patches_SGTs
    {

        [HarmonyPatch(typeof(PauseScreen))]
        [HarmonyPatch(nameof(PauseScreen.OnKeyDown))]
        public static class CatchGoingBack
        {
            public static bool Prefix(KButtonEvent e)
            {
                if (CustomSettingsController.Instance != null && CustomSettingsController.Instance.CurrentlyActive)
                    return false;
                return true;
            }
        }

        private static readonly KButtonMenu.ButtonInfo TwitchButtonInfo = new KButtonMenu.ButtonInfo((string)"BUTTONTEXT", Action.NumActions, new UnityAction(OnCustomMenuButtonPressed));
        private static void OnCustomMenuButtonPressed()
        {
            PauseScreen.Instance.RefreshButtons();
            CustomSettingsController.ShowWindow();
            GameScheduler.Instance.ScheduleNextFrame("OpenCustomSettings", (System.Action<object>)(_ =>
            {
                PauseScreen.Instance.RefreshButtons();
            }));
        }

        [HarmonyPatch(typeof(PauseScreen), "OnPrefabInit")]
        private static class PauseScreen_OnPrefabInit_Patch
        {
            [UsedImplicitly]
            private static void Postfix(ref IList<KButtonMenu.ButtonInfo> ___buttons)
            {
                List<KButtonMenu.ButtonInfo> list = ___buttons.ToList<KButtonMenu.ButtonInfo>();
                TwitchButtonInfo.isEnabled = true;
                list.Insert(5, TwitchButtonInfo);
                ___buttons = (IList<KButtonMenu.ButtonInfo>)list;
            }
        }
    }
}
