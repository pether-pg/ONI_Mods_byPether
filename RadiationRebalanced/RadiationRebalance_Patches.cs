using HarmonyLib;
using UnityEngine;
using System;

namespace RadiationRebalanced
{
    public class RadiationRebalance_Patches
    {
		[HarmonyPatch(typeof(HiveEatingStates.Instance))]
		[HarmonyPatch("TurnOn")]
		public class HiveEatingStatesInstance_TurnOn_Patch
		{
			public static void Postfix(HiveEatingStates.Instance __instance)
			{
				RadiationEmitter emitter = Traverse.Create(__instance).Field("emitter").GetValue<RadiationEmitter>();
				Settings.Instance.BeeHiveEating.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(HiveEatingStates.Instance))]
		[HarmonyPatch("TurnOff")]
		public class HiveEatingStatesInstance_TurnOff_Patch
		{
			public static void Postfix(HiveEatingStates.Instance __instance)
			{
				RadiationEmitter emitter = Traverse.Create(__instance).Field("emitter").GetValue<RadiationEmitter>();
				Settings.Instance.BeeHiveIdle.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(GlowStick.StatesInstance))]
		[HarmonyPatch(MethodType.Constructor)]
		[HarmonyPatch(new Type[] { typeof(GlowStick) })]
		public class GlowStickStatesInstance_Constructor_Patch
		{
			public static void Postfix(GlowStick.StatesInstance __instance)
			{
				RadiationEmitter _radiationEmitter = Traverse.Create(__instance).Field("_radiationEmitter").GetValue<RadiationEmitter>();
				Settings.Instance.GlowStick.Emitter.ApplySetting(_radiationEmitter);
				_radiationEmitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(ColdBreatherConfig))]
		[HarmonyPatch("CreatePrefab")]
		public class ColdBreatherConfig_CreatePrefab_Patch
		{
			public static void Postfix(GameObject __result)
			{
				RadiationEmitter emitter = __result.GetComponent<RadiationEmitter>();
				Settings.Instance.WheezewortGrowing.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(BaseBeeConfig))]
		[HarmonyPatch("BaseBee")]
		public class BaseBeeConfig_BaseBee_Patch
		{
			public static void Postfix(GameObject __result, bool is_baby)
			{
				RadiationEmitter emitter = __result.GetComponent<RadiationEmitter>();
				float? beeRadiationOutputAmount;
				if(is_baby)
				{
					Settings.Instance.Beetiny.ApplySetting(emitter);
					beeRadiationOutputAmount = Settings.Instance.Beetiny.emitRads;
				}
				else
				{
					Settings.Instance.Beeta.ApplySetting(emitter);
					beeRadiationOutputAmount = Settings.Instance.Beeta.emitRads;
				}
				emitter.Refresh();

				Bee bee = __result.GetComponent<Bee>();
				if (bee != null && beeRadiationOutputAmount.HasValue)
					bee.radiationOutputAmount = beeRadiationOutputAmount.Value;
			}
		}

		[HarmonyPatch(typeof(BaseLightBugConfig))]
		[HarmonyPatch("BaseLightBug")]
		public class BaseLightBugConfig_BaseLightBug_Patch
		{
			public static void Postfix(GameObject __result)
			{
				RadiationEmitter emitter = __result.GetComponent<RadiationEmitter>();
				Settings.Instance.Shinebug.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(RadiationLightConfig))]
		[HarmonyPatch("ConfigureBuildingTemplate")]
		public class RadiationLightConfig_ConfigureBuildingTemplate_Patch
		{
			public static void Postfix(GameObject go)
			{
				RadiationEmitter emitter = go.GetComponent<RadiationEmitter>();
				Settings.Instance.RadiationLamp.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(HEPEngineConfig))]
		[HarmonyPatch("DoPostConfigureComplete")]
		public class HEPEngineConfig_DoPostConfigureComplete_Patch
		{
			public static void Postfix(GameObject go)
			{
				RadiationEmitter emitter = go.GetComponent<RadiationEmitter>();
				Settings.Instance.NuclearRocketEngine.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(NuclearReactorConfig))]
		[HarmonyPatch("DoPostConfigureComplete")]
		public class NuclearReactorConfig_DoPostConfigureComplete_Patch
		{
			public static void Postfix(GameObject go)
			{
				RadiationEmitter emitter = go.GetComponent<RadiationEmitter>();
				Settings.Instance.ResearchReactor.ApplySetting(emitter);
				emitter.Refresh();
			}
		}

		[HarmonyPatch(typeof(Reactor))]
		[HarmonyPatch("SetEmitRads")]
		public class Reactor_SetEmitRads_Patch
		{
			public static void Prefix(ref float rads)
			{
				if (rads == 2400f && Settings.Instance.ResearchReactor.emitRads.HasValue)
					rads = Settings.Instance.ResearchReactor.emitRads.Value;
			}
		}
	}
}
