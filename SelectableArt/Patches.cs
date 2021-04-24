using Harmony;
using System;
using UnityEngine;

namespace SelectableArt
{
    public class Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
                Debug.Log("SelectableArt: Loaded.");
            }
        }
        [HarmonyPatch(typeof(Artable), "OnCompleteWork")]
        public static class Artable_OnCompleteWork_Patch
        {
            internal static void Postfix(Artable __instance)
            {
                GameObject gameObject = __instance.gameObject;
                if (!((UnityEngine.Object)gameObject != (UnityEngine.Object)null))
                    return;
                Game.Instance.userMenu?.Refresh(gameObject);
            }
        }

        [HarmonyPatch(typeof(Artable), "OnSpawn")]
        public static class Artable_OnSpawn_Patch
        {
            internal static void Postfix(Artable __instance)
            {
                PickableLook pickable = __instance.gameObject.AddOrGet<PickableLook>();
                pickable.artable = __instance;
            }
        }
    }
}
