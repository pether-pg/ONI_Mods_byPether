using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;

namespace CombinedConduitDisplay
{
    class ZAxis
    {
        public static Dictionary<SaveLoadRoot, float> InitialZValues = new Dictionary<SaveLoadRoot, float>();

        public static void ForceBehindTag(GameObject go)
        {
            if (go.GetComponent<KPrefabID>().HasTag(GameTags.OverlayInFrontOfConduits))
                go.GetComponent<KPrefabID>().RemoveTag(GameTags.OverlayInFrontOfConduits);
            go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits);
        }

        public static void ModifyVectorZAxis(SaveLoadRoot layerTarget)
        {
            if (layerTarget == null)
                return;

            KPrefabID prefab = layerTarget.GetComponent<KPrefabID>();
            if (prefab == null || !prefab.HasTag(GameTags.OverlayBehindConduits))
                return;

            Vector3 position = layerTarget.transform.GetPosition();
            float desired = Grid.GetLayerZ(Grid.SceneLayer.SolidConduits) + 0.2f;

            if (position.z != desired)
            {
                if (!InitialZValues.ContainsKey(layerTarget))
                    InitialZValues.Add(layerTarget, position.z);

                position.z = desired;
                layerTarget.transform.SetPosition(position);
                KanimRefresh.RefreshKbacForLayerTarget(layerTarget.gameObject);
            }
        }

        public static void RestoreInitialZAxis(SaveLoadRoot layerTarget)
        {
            if (layerTarget == null)
                return;

            if (!InitialZValues.ContainsKey(layerTarget))
                return;

            Vector3 position = layerTarget.transform.GetPosition();
            position.z = InitialZValues[layerTarget];

            layerTarget.transform.SetPosition(position);
            KanimRefresh.RefreshKbacForLayerTarget(layerTarget.gameObject);
        }
    }
}
