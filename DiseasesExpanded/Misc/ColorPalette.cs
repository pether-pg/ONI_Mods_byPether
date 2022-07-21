using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    public class ColorPalette
    {
        public static Color32 BloodyRed = new Color32(255, 0, 0, 255);
        public static Color32 FreshGreen = new Color32(0, 255, 0, 255);
        public static Color32 DeepBlue = new Color32(0, 0, 255, 255);
        public static Color32 BrightYellow = new Color32(255, 255, 0, 255);
        public static Color32 PureCyan = new Color32(0, 255, 255, 255);
        public static Color32 SnowWhite = new Color32(255, 255, 255, 255);
        public static Color32 PaleGreen = new Color32(128, 255, 128, 255);
        public static Color32 MidGray = new Color32(128, 128, 128, 255);
        public static Color32 HalfRed = new Color32(128, 0, 0, 255);
        public static Color32 QuaterRed = new Color32(64, 0, 0, 255);
        public static Color32 ReddyPurple = new Color32(255, 0, 128, 255);

        public static Color32 NavyBlue = new Color32(0, 0, 128, 255);
        public static Color32 DeepPurple = new Color32(128, 0, 128, 255);

        public static Color32 HungryBrown = new Color32(110, 110, 65, 255);
        public static Color32 BogViolet = new Color32(126, 36, 76, 255);
        public static Color32 IcyBlue = new Color32(176, 230, 230, 255);
        public static Color32 GassyOrange = new Color32(208, 138, 74, 255);
        public static Color32 ChlorineGreen = new Color32(178, 215, 144, 255);

        // Existing in game, gathered from ColorSet.namedLookup
        public static Color32 PoisonYellow = new Color32(255, 231, 47, 255);
        public static Color32 FlowerPink = new Color32(228, 155, 241, 255);
        public static Color32 SlimelungGreen = new Color32(59, 254, 149, 255);
        public static Color32 ZombieBlue = new Color32(148, 183, 255, 255);
        public static Color32 RadiationGreen = new Color32(134, 226, 86, 255);
        public static Color32 HospitalPink = new Color32(255, 132, 142, 255);

        public static Color ToColor(Color32 color32)
        {
            return new Color(
                color32.r / 255.0f, 
                color32.g / 255.0f, 
                color32.b / 255.0f,
                color32.a / 255.0f
            );
        }
    }
}
