using System;
using System.Collections.Generic;
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

        /*
         * Previous solution
         * Might be reused one day
         * 

        protected override void OnCleanUp()
        {
            this.Unsubscribe(493375141);
            base.OnCleanUp();
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe(493375141, new System.Action<object>(this.OnRefreshUserMenu));
        }

        private void OnRefreshUserMenu(object obj)
        {
            string nextIcon = "action_direction_right";
            string backIcon = "action_direction_left";

            int count = animsWithNames.Keys.Count;
            if (count > 1) Game.Instance?.userMenu?.AddButton(this.gameObject, new KIconButtonMenu.ButtonInfo(nextIcon, INTERNALSTRINGS.NEXT_ART_BUTTON.TEXT, new System.Action(this.OnNextArtClicked), Action.BuildMenuKeyQ, tooltipText: ((string)INTERNALSTRINGS.NEXT_ART_BUTTON.TOOLTIP)));
            if (count > 1) Game.Instance?.userMenu?.AddButton(this.gameObject, new KIconButtonMenu.ButtonInfo(backIcon, INTERNALSTRINGS.BACK_ART_BUTTON.TEXT, new System.Action(this.OnBackArtClicked), Action.BuildMenuKeyR, tooltipText: ((string)INTERNALSTRINGS.BACK_ART_BUTTON.TOOLTIP)));
        }

        private void OnNextArtClicked()
        {
            // Select next variant
        }

        private void OnBackArtClicked()
        {
            // Select previous variant
        }*/
    }
}
