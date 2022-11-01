using System.Collections.Generic;
using UnityEngine;

namespace SignsTagsAndRibbons
{
    // Using Aki's code as an example: https://github.com/aki-art/ONI-Mods/blob/master/DecorPackA/Buildings/MoodLamp/MoodLampSideScreen.cs
    class SignSideScreen : SideScreenContent
    {
        [SerializeField]
        private RectTransform buttonContainer;

        private GameObject stateButtonPrefab;
        private GameObject debugVictoryButton;
        private GameObject flipButton;
        private readonly List<GameObject> buttons = new List<GameObject>();
        private SelectableSign target;

        public const string SCREEN_TITLE_KEY = "STRINGS.UI.UISIDESCREENS.SIGN_SIDE_SCREEN.TITLE";

        public override bool IsValidForTarget(GameObject target)
        {
            return target.GetComponent<SelectableSign>() != null;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn(); 

            // the monument screen used here has 2 extra buttons that are not needed, disabling them
            flipButton.SetActive(false);
            debugVictoryButton.SetActive(false);
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            titleKey = SCREEN_TITLE_KEY;
            stateButtonPrefab = transform.Find("ButtonPrefab").gameObject;
            buttonContainer = transform.Find("Content/Scroll/Grid").GetComponent<RectTransform>();
            debugVictoryButton = transform.Find("Butttons/Button").gameObject;
            flipButton = transform.Find("Butttons/FlipButton").gameObject;
        }

        public override void SetTarget(GameObject target)
        {
            base.SetTarget(target);
            this.target = target.GetComponent<SelectableSign>();
            gameObject.SetActive(true);
            GenerateStateButtons();
        }
        
        private void GenerateStateButtons()
        {
            ClearButtons();
            var animFile = target.GetComponent<KBatchedAnimController>().AnimFiles[0];

            foreach (string anim in target.AnimationNames)
            {
                AddButton(animFile, anim, anim, () => target.SetVariant(anim));
            }
        }
        private void AddButton(KAnimFile animFile, string animName, LocString tooltip, System.Action onClick)
        {
            var gameObject = Util.KInstantiateUI(stateButtonPrefab, buttonContainer.gameObject, true);

            if (gameObject.TryGetComponent(out KButton button))
            {
                button.onClick += onClick;
                button.fgImage.sprite = Def.GetUISpriteFromMultiObjectAnim(animFile, animName);
            }

            //FUI_SideScreen.AddSimpleToolTip(gameObject, tooltip, true);
            buttons.Add(gameObject);
        }

        private void ClearButtons()
        {
            foreach (var button in buttons)
            {
                Util.KDestroyGameObject(button);
            }

            buttons.Clear();

            flipButton.SetActive(false);
            debugVictoryButton.SetActive(false);
        }
    }
}
