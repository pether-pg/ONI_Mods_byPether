using HarmonyLib;
using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Database.MonumentPartResource;
using static DetailsScreen;
using static KAnim.Build;

namespace SidequestMod
{
    class UIUtils
    {
        public static ToolTip AddSimpleTooltipToObject(Transform go, string tooltip, bool alignCenter = false, float wrapWidth = 0, bool onBottom = false)
        {
            if (go == null)
                return null;
            var tt = go.gameObject.AddOrGet<ToolTip>();
            tt.UseFixedStringKey = false;
            tt.enabled = true;
            tt.tooltipPivot = alignCenter ? new Vector2(0.5f, onBottom ? 1f : 0f) : new Vector2(1f, onBottom ? 1f : 0f);
            tt.tooltipPositionOffset = onBottom ? new Vector2(0f, -20f) : new Vector2(0f, 20f);
            tt.parentPositionAnchor = new Vector2(0.5f, 0.5f);
            if (wrapWidth > 0)
            {
                tt.WrapWidth = wrapWidth;
                tt.SizingSetting = ToolTip.ToolTipSizeSetting.MaxWidthWrapContent;
            }
            ToolTipScreen.Instance.SetToolTip(tt);
            tt.SetSimpleTooltip(tooltip);
            return tt;
        }
    }
}
