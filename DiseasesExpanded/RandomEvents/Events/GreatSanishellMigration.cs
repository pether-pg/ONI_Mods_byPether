using System;
using UnityEngine;
using ONITwitchLib.Utils;
using STRINGS;

namespace DiseasesExpanded.RandomEvents.Events
{
    class GreatSanishellMigration : RandomDiseaseEvent
    {
        public GreatSanishellMigration(int weight = 1)
        {
            ID = nameof(GreatSanishellMigration);
            GeneralName = STRINGS.RANDOM_EVENTS.GREAT_SANISHELL_MIGRATION.NAME;
            NameDetails = Util.StripTextFormatting(CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.NAME);
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    int numberOfSpawns = 10;
                    int acceptableRetries = 1000;
                    Vector3 min = PosUtil.CameraMinWorldPos();
                    Vector3 max = PosUtil.CameraMaxWorldPos();
                    Vector3 rainMin = min + new Vector3((max.x - min.x) * 0.2f, (max.y - min.y) * 0.6f);
                    Vector3 rainMax = max + new Vector3(-(max.x - min.x) * 0.2f, -(max.y - min.y) * 0.2f);

                    for(int i=0; i<numberOfSpawns; i++)
                    {
                        int randPosCell = Grid.XYToCell(
                            (int)UnityEngine.Random.Range(rainMin.x, rainMax.x),
                            (int)UnityEngine.Random.Range(rainMin.y, rainMax.y)
                        );

                        int targetCell = GridUtil.NearestEmptyCell(randPosCell);
                        int aboveCell = Grid.CellAbove(targetCell);

                        if(Grid.IsSolidCell(aboveCell) && acceptableRetries > 0)
                        {
                            i--;
                            acceptableRetries--;
                            continue;
                        }

                        GameObject crab = GameUtil.KInstantiate(Assets.GetPrefab(CrabFreshWaterConfig.ID), Grid.CellToPos(targetCell), Grid.SceneLayer.Creatures);
                        crab.SetActive(true);
                        GameObject egg = GameUtil.KInstantiate(Assets.GetPrefab(CrabFreshWaterConfig.EGG_ID), Grid.CellToPos(targetCell), Grid.SceneLayer.Creatures);
                        egg.SetActive(true);
                    }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.GREAT_SANISHELL_MIGRATION.TOAST);
                });
        }
    }
}
