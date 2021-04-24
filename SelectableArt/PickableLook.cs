using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelectableArt
{
    class PickableLook : KMonoBehaviour
    {
        public Artable artable;
        //[MyCmpAdd]
        //private CopyBuildingSettings copyBuildingSettings; // even if never used, this is required to coppy settings
        //private static readonly EventSystem.IntraObjectHandler<PickableLook> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<PickableLook>((System.Action<PickableLook, object>)((component, data) => component.OnCopySettings(data)));

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

        /*private void OnCopySettings(object data)
        {
            throw new NotImplementedException("This one is not thrown...");
            PickableLook component = ((UnityEngine.GameObject)data).GetComponent<PickableLook>();
            if (!((UnityEngine.Object)component != (UnityEngine.Object)null))
                return;
            this.artable.SetStage(component.artable.CurrentStage, false);
            Debug.Log("SelectableArt: copied");
        }*/

        private void OnNextArtClicked()
        {
            Artable.Status status = artable.stages.Find(s => s.id == artable.CurrentStage).statusItem;
            List<Artable.Stage> potential_stages = new List<Artable.Stage>();
            foreach (Artable.Stage s in artable.stages)
                if (s.statusItem == status)
                    potential_stages.Add(s);

            int i = potential_stages.FindIndex(s => s.id == artable.CurrentStage);
            Artable.Stage desiredStage = (i + 1 == potential_stages.Count) ? potential_stages[0] : potential_stages[i + 1];
            artable.SetStage(desiredStage.id, false);
        }

        private void OnRotateClicked()
        {
            Rotatable rotatable = this.gameObject.GetComponent<Rotatable>();
            if (rotatable != null) rotatable.Rotate();

            BuildingDef def = this.gameObject.GetComponent<Building>()?.Def;
            if (def != null && def.WidthInCells % 2 == 0)
                this.transform.position += rotatable.GetOrientation() != Orientation.Neutral ? new UnityEngine.Vector3(1, 0, 0) : new UnityEngine.Vector3(-1, 0, 0);

            artable.SetStage(artable.CurrentStage, false);
        }

        private void OnRefreshUserMenu(object _)
        {
            if (!((UnityEngine.Object)this.artable != (UnityEngine.Object)null) || this.artable.CurrentStatus == Artable.Status.Ready)
                return;
            string nextIcon = "action_direction_right";
            string rotateIcon = "action_direction_both";

            Artable.Status status = artable.stages.Find(s => s.id == artable.CurrentStage).statusItem;
            int count = artable.stages.Where(s => s.statusItem == status).ToList().Count;
            if (count > 1)
                Game.Instance?.userMenu?.AddButton(this.gameObject, new KIconButtonMenu.ButtonInfo(nextIcon, STRINGS.NEXT_ART_BUTTON.TEXT, new System.Action(this.OnNextArtClicked), Action.BuildMenuKeyQ, tooltipText: ((string)STRINGS.NEXT_ART_BUTTON.TOOLTIP)));
            if(this.gameObject.GetComponent<Rotatable>() != null)
                Game.Instance?.userMenu?.AddButton(this.gameObject, new KIconButtonMenu.ButtonInfo(rotateIcon, STRINGS.ROTATE_ART_BUTTON.TEXT, new System.Action(this.OnRotateClicked), Action.BuildMenuKeyO, tooltipText: ((string)STRINGS.ROTATE_ART_BUTTON.TOOLTIP)));
        }
    }
}
