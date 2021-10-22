using System;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;

namespace ConveyorRailDisplay
{
    class ManualPatching
    {
        public static MethodInfo GetMethodInfo(string className, string methodName)
        {
            Type classType = Type.GetType(string.Format("{0}, Assembly-CSharp", className), false);
            if (classType == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Error - {className} type is null...");
                return null;
            }
            return GetMethodInfo(classType, methodName);
        }

        public static MethodInfo GetMethodInfo(Type classType, string methodName)
        {
            MethodInfo method = classType.GetMethod(methodName);
            if (method == null)
                Debug.Log($"{ModInfo.Namespace}: Error - {methodName} method is null...");

            return method;
        }

        public static void ManualPatch(Harmony harmony, MethodInfo patched, MethodInfo prefix, MethodInfo postfix)
        {
            harmony.Patch(patched,
                    prefix == null ? null : new HarmonyMethod(prefix),
                    postfix == null ? null : new HarmonyMethod(postfix));
        }
    }
}
