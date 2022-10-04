using UnityEngine;

namespace InterplanarInfrastructure
{
    class SolarLenseSatelite : StateMachineComponent<SolarLenseSatelite.StatesInstance>
    {

        protected override void OnSpawn()
        {
            this.smi.StartSM();
        }

        public class StatesInstance : GameStateMachine<SolarLenseSatelite.States, SolarLenseSatelite.StatesInstance, SolarLenseSatelite, object>.GameInstance
        {
            public StatesInstance(SolarLenseSatelite smi)
              : base(smi)
            {
                this.world = this.gameObject.GetMyWorld();
                this.hitEffectPrefab = Assets.GetPrefab((Tag)"fx_powertinker_splash");

                KBatchedAnimController kbac = this.gameObject.GetComponent<KBatchedAnimController>();
                if (kbac != null)
                    kbac.TintColour = new Color32(255, 255, 0, 255);
            }

            private int lastFocusedCell = Grid.InvalidCell;
            private const float KJ_PER_LUX = 0.005f;

            private GameObject hitEffect;
            private GameObject hitEffectPrefab;
            private WorldContainer world;


            public float MaxTemperature = Sim.MaxTemperature;
            public Vector2I SolarLanceOffset = new Vector2I(0, 0);

            public void UpdateSatelite(float dt)
            {
                if (world == null)
                    world = this.gameObject.GetMyWorld();

                int cell = RefocusLense();
                float energy = CalculateHeatEnergy(dt);

                if (energy > 0)
                {
                    HeatCell(cell, energy);
                    if (cell != lastFocusedCell || hitEffect == null)
                    {
                        lastFocusedCell = cell;
                        CreateHitEffect();
                    }
                }
                else
                    DestroyHitEffect();
            }

            public int RefocusLense()
            {
                if (world == null)
                    return Grid.InvalidCell;

                int testingCell = Grid.OffsetCell(Grid.PosToCell(this.gameObject), SolarLanceOffset.x, SolarLanceOffset.y);

                while (Grid.IsValidCellInWorld(testingCell, world.id))
                {
                    if (Grid.IsLiquid(testingCell) || Grid.IsSolidCell(testingCell))
                        return testingCell;
                    testingCell = Grid.OffsetCell(testingCell, 0, -1);
                }

                return Grid.InvalidCell;
            }

            public bool IsIlluminated()
            {
                return world.currentSunlightIntensity > 0;
            }

            public float CalculateHeatEnergy(float dt)
            {
                float currentLux = world.currentSunlightIntensity;
                return currentLux * KJ_PER_LUX * dt;
            }

            public void HeatCell(int cell, float energy)
            {
                if (cell == Grid.InvalidCell || world == null)
                    return;

                SimMessages.ModifyEnergy(cell, energy, MaxTemperature, SimMessages.EnergySourceID.HeatBulb);
            }

            private void CreateHitEffect()
            {
                if (this.hitEffectPrefab == null)
                    return;
                if (this.hitEffect != null)
                    this.DestroyHitEffect();
                this.hitEffect = GameUtil.KInstantiate(this.hitEffectPrefab, Grid.CellToPosCCC(lastFocusedCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2);
                this.hitEffect.SetActive(true);

                KBatchedAnimController component = this.hitEffect.GetComponent<KBatchedAnimController>();
                component.sceneLayer = Grid.SceneLayer.FXFront2;
                component.initialMode = KAnim.PlayMode.Loop;
                component.enabled = false;
                component.enabled = true;

                LoopingSounds sound = hitEffect.GetComponent<LoopingSounds>();
                if (sound != null)
                    sound.vol = 0;
            }

            private void DestroyHitEffect()
            {
                if (this.hitEffectPrefab == null || !(this.hitEffect != null))
                    return;
                this.hitEffect.DeleteObject();
                this.hitEffect = null;
            }

            public string GetStatusItemProgress()
            {
                if (world == null)
                    return string.Empty;
                return string.Format("{0}000 lux", (int)world.currentSunlightIntensity / 1000);
            }

            public void Log(string msg)
            {
                Debug.Log($"InterplanarInfrastructure: SolarLenseSatelite: {msg}");
                PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, msg, this.gameObject.transform);
            }
        }

        public class States : GameStateMachine<SolarLenseSatelite.States, SolarLenseSatelite.StatesInstance, SolarLenseSatelite>
        {
            public State off;
            public State on;
            public override void InitializeStates(out StateMachine.BaseState default_state)
            {
                default_state = (StateMachine.BaseState)this.off;
                this.off
                    .PlayAnim("off")
                    .Enter(smi => smi.Log("enter off"))
                    .Transition(this.on, (smi) => smi.IsIlluminated() && WorldBorderChecker.IsInTopOfTheWorld(Grid.PosToCell(smi.gameObject.transform.position)));
                this.on
                    .ToggleStatusItem(Db.Get().BuildingStatusItems.Get(SolarLenseSateliteConfig.StatusItemID), (smi => smi))
                    .PlayAnim("working_loop", KAnim.PlayMode.Loop)
                    .Enter(smi => smi.Log("enter on"))
                    .Update((smi, dt) => smi.UpdateSatelite(dt))
                    .Transition(this.off, (smi) => !smi.IsIlluminated());
            }
        }
    }
}
