using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.EntityScripts
{
    class FirstAidKitOpener : KMonoBehaviour
    {
        protected override void OnCleanUp()
        {
            this.Unsubscribe((int)GameHashes.RefreshUserMenu);
            base.OnCleanUp();
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe((int)GameHashes.RefreshUserMenu, new System.Action<object>(this.OnRefreshUserMenu));
        }

        private void OnRefreshUserMenu(object obj)
        {
            string nextIcon = "action_direction_right";
            string text = STRINGS.RANDOM_EVENTS_ENTITYSCRIPTS.FIRSTAIDKITOPENER.TEXT;
            string tooltip = STRINGS.RANDOM_EVENTS_ENTITYSCRIPTS.FIRSTAIDKITOPENER.TOOLTIP;

            Game.Instance?.userMenu?.AddButton(this.gameObject, new KIconButtonMenu.ButtonInfo(nextIcon, text, new System.Action(this.OpenFirstAidKit), Action.BuildMenuKeyQ, tooltipText: tooltip));
        }

        private void OpenFirstAidKit()
        {
            Dictionary<string, int> PossibleSpawns = new Dictionary<string, int>();
            PossibleSpawns.Add(LightBugBlackConfig.EGG_ID, 3);
            PossibleSpawns.Add(MoleDelicacyConfig.EGG_ID, 3);
            PossibleSpawns.Add(LightBugConfig.EGG_ID, 3);
            PossibleSpawns.Add(PrickleFlowerConfig.SEED_ID, 3);
            PossibleSpawns.Add(AlienSicknessCureConfig.ID, 10);
            PossibleSpawns.Add(HappyPillConfig.ID, 10);
            PossibleSpawns.Add(MutatingAntiviralConfig.ID, 10);
            PossibleSpawns.Add(SerumSuperConfig.ID, 10);
            PossibleSpawns.Add(AdvancedCureConfig.ID, 10);

            List<string> possibleIds = new List<string>();
            foreach (string key in PossibleSpawns.Keys)
                possibleIds.Add(key);

            possibleIds.Shuffle();
            string randomId = possibleIds[0];

            for (int i = 0; i < PossibleSpawns[randomId]; i++)
            {
                GameObject spawn = GameUtil.KInstantiate(Assets.GetPrefab(randomId), this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Grid.SceneLayer.BuildingFront);
                spawn.SetActive(true);
            }

            PlayDestroyAnim();
            Util.KDestroyGameObject(this.gameObject);
        }

        private void PlayDestroyAnim()
        {
            GameObject hitEffectPrefab = Assets.GetPrefab("fx_tend_splash");
            GameObject hitEffect = GameUtil.KInstantiate(hitEffectPrefab, this.gameObject.transform.position, Grid.SceneLayer.FXFront2);
            hitEffect.SetActive(true);
            KBatchedAnimController kbac = hitEffect.GetComponent<KBatchedAnimController>();
            kbac.PlayMode = KAnim.PlayMode.Once;
            kbac.destroyOnAnimComplete = true;
        }
    }
}
