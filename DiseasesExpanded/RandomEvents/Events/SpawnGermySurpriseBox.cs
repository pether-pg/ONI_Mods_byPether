using System;
using UnityEngine;
using DiseasesExpanded.RandomEvents.Configs;
using DiseasesExpanded.RandomEvents.EntityScripts;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SpawnGermySurpriseBox : RandomDiseaseEvent
    {
        public SpawnGermySurpriseBox(byte germIdx, int weight = 1)
        {
            GeneralName = GermySurpriseBoxConfig.TryGetName();
            NameDetails = Db.Get().Diseases[germIdx].Id;
            ID = GenerateId(nameof(AdoptStrayPet), NameDetails);
            Group = nameof(SpawnGermySurpriseBox);
            AppearanceWeight = weight;
            DangerLevel = Helpers.EstimateGermDanger(germIdx);

            Condition = new Func<object, bool>(data => DlcManager.IsExpansion1Active() && GameClock.Instance.GetCycle() > (int)DangerLevel * 100);

            Event = new Action<object>(
                data =>
                {
                    GameObject box = GameUtil.KInstantiate(
                        Assets.GetPrefab(GermySurpriseBoxConfig.ID),
                        Grid.CellToPosCBC(GetSpawnCell(), Grid.SceneLayer.Front), 
                        Grid.SceneLayer.Front);

                    GermySurpriseBox boxCmp = box.GetComponent<GermySurpriseBox>();
                    if (boxCmp != null)
                        boxCmp.DiseaseIdx = germIdx;

                    box.SetActive(true);

                    ONITwitchLib.ToastManager.InstantiateToastWithPosTarget(GetToastTitle(), GetToastBody(), box.transform.position);
                });
        }

        string GetToastTitle()
        {
            return "Surprise Box";
        }

        string GetToastBody()
        {
            return "I wonder what is inside?";
        }

        int GetSpawnCell()
        {
            // Made the same as in https://github.com/asquared31415/ONITwitch/blob/main/ONITwitchCore/Commands/SurpriseBoxCommand.cs
            
            if (Components.Telepads.Count > 0)
                return Grid.CellAbove(Grid.PosToCell(Components.Telepads.Items.GetRandom()));

            if (Components.LiveMinionIdentities.Count > 0)
                return Grid.PosToCell(Components.LiveMinionIdentities.Items.GetRandom());
                
            Debug.Log($"{ModInfo.Namespace}: Unable to spawn a Surprise Box, no telepads or live minions");
            return Grid.InvalidCell;            
        }
    }
}
