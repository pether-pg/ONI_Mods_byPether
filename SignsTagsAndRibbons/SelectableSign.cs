using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using KSerialization;

namespace SignsTagsAndRibbons
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class SelectableSign : KMonoBehaviour
    {
        [MyCmpGet]
        KBatchedAnimController kbac;

        [Serialize]
        public List<string> AnimationNames = new List<string>();

        [Serialize]
        public int selectedIndex = 0;

        const string VARIANT_KEY = "Selected_Variant";


        protected override void OnSpawn()
        {
            if (AnimationNames == null || AnimationNames.Count == 0)
                return;

            if (selectedIndex >= AnimationNames.Count)
                selectedIndex = 0;

            kbac.Play(AnimationNames[selectedIndex]);
        }

        public void SetVariant(string variant)
        {
            if (!AnimationNames.Contains(variant))
                return;

            selectedIndex = AnimationNames.FindIndex(str => str == variant);
            kbac.Play(variant);
        }

        public string GetCurrentVariant()
        {
            if (AnimationNames == null || AnimationNames.Count <= selectedIndex)
                return string.Empty;
            return AnimationNames[selectedIndex];
        }

        public static void Blueprints_SetData(GameObject source, JObject data)
        {
            if (source.TryGetComponent<SelectableSign>(out var behavior))
            {
                var t1 = data.GetValue(VARIANT_KEY);
                if (t1 == null)
                    return;

                string variant = t1.Value<string>();
                behavior.SetVariant(variant);
            }
        }

        public static JObject Blueprints_GetData(GameObject source)
        {
            if (source.TryGetComponent<SelectableSign>(out var behavior))
            {
                return new JObject()
                {
                    { VARIANT_KEY, behavior.GetCurrentVariant()}
                };
            }
            return null;
        }
    }
}
