using System;

namespace SignsTagsAndRibbons
{
    class STRINGS
    {
        public class SIDESCREEN
        {
            public static LocString TITLE = "Select Variant";
        }

        public class BUILDINGS
        {
            public class DANGERRIBBON
            {
                public static LocString NAME = "Danger Ribbon";
                public static LocString DESC = "A danger ribbon element.";
                public static LocString EFFECT = "It serves to demark a straight line used to outline a dangerous area.";
            }

            public class DANGERCORNER
            {
                public static LocString NAME = "Danger Ribbon Corner";
                public static LocString DESC = "A danger ribbon mark for corners.";
                public static LocString EFFECT = "It serves to demark a corner segment of a dangerous ribbon line.";
            }

            public class METERSCALE
            {
                public static LocString NAME = "Meter Scale";
                public static LocString DESC = "A wall meter scale used in open liquid reservoirs.";
                public static LocString EFFECT = "This scale is often used to measure the amount of liquid in a reservoir.";
            }

            public class INFO
            {
                public static LocString NAME = "Info Sign";
                public static LocString DESC = "A wall sign for directive environmental information.";
                public static LocString EFFECT = "This sign is a visual representation many different directive informations regarding environment. Select the type of directive using the menu option. \n\nList of tags: " +
                    "\n - Atmo Suit"+
                    "\n - Jet Suit"+
                    "\n - Lead Suit"+
                    "\n - Dormitory"+
                    "\n - Infirmary"+
                    "\n - Workshop"+
                    "\n - Research Facility"+
                    "\n - Observatory"+
                    "\n - Restroom"+
                    "\n - Refectory"+
                    "\n - Farm"+
                    "\n - Critter Ranch"+
                    "\n - Rocket Platform";
            }

            public class SYSTEM
            {
                public static LocString NAME = "System Tag";
                public static LocString DESC = "A wall tag for system and sensors.";
                public static LocString EFFECT = "This tag is a visual representation of many different kinds of eletronics, systems and sensors signs. Select the type of system using the menu option. \n\nList of tags: " +
                    "\n - Closed Cycle" +
                    "\n - Organic Recycle" +
                    "\n - Recycle" +
                    "\n - Power Toggle" +
                    "\n - Gas Element Sensor" +
                    "\n - Liquid Sensor" +
                    "\n - Power Sensor" +
                    "\n - Pressure Sensor" +
                    "\n - Temperature Sensor" +
                    "\n - Time Sensor";
            }

            public class UTILITY
            {
                public static LocString NAME = "Utility Tag";
                public static LocString DESC = "A wall tag for utilities.";
                public static LocString EFFECT = "This tag is a visual representation of many different kinds of utilities signs. Select the type of utility using the menu option. \n\nList of tags: " +
                    "\n - Direction" +
                    "\n - Diagonal Direction" +
                    "\n - Both Direction" +
                    "\n - Gas Pump" +
                    "\n - Liquid Pump" +
                    "\n - Solid Conveyor";
            }

            public class ALERT
            {
                public static LocString NAME = "Danger Sign";
                public static LocString DESC = "A wall sign for alerts and dangers.";
                public static LocString EFFECT = "This sign is a visual representation of many different alerts. Select the type of alert using the menu option. \n\nList of tags: " +
                    "\n - Warning" +
                    "\n - Hot Spot" +
                    "\n - Cold Spot" +
                    "\n - Comet Alert" +
                    "\n - Biohazard Alert" +
                    "\n - Radiation Alert" +
                    "\n - Brightness Alert" +
                    "\n - Electricity Alert" +
                    "\n - Danger Alert";
            }

            public class SOLID
            {
                public static LocString NAME = "Solid Tag";
                public static LocString DESC = "A wall tag for solids.";
                public static LocString EFFECT = "This tag is a visual representation of solids in an industrial environment. Select the type of solids using the menu option. \n\nList of tags: " +
                    "\n - Agriculture" +
                    "\n - Consumable Ore" +
                    "\n - Critter Egg" +
                    "\n - Cultivable Soil" +
                    "\n - Filtration Medium" +
                    "\n - Industrial Ingredient" +
                    "\n - Liquefiable" +
                    "\n - Manufacture Material" +
                    "\n - Medical Supplies" +
                    "\n - Metal Ore" +
                    "\n - Miscellaneous" +
                    "\n - Organic" +
                    "\n - Rare Resource" +
                    "\n - Raw Mineral" +
                    "\n - Refined Metal" +
                    "\n - Refined Mineral" +
                    "\n - Cooking Ingredient" +
                    "\n - Edibles" +
                    "\n - Seeds" +
                    "\n - Compostables";
            }

            public class LIQUID
            {
                public static LocString NAME = "Liquid Tag";
                public static LocString DESC = "A wall tag for liquids.";
                public static LocString EFFECT = "This tag is a visual representation of liquids in an industrial environment. Select the type of liquid using the menu option. \n\nList of tags: " +
                    "\n - Water" +
                    "\n - Polluted Water" +
                    "\n - Salt Water" +
                    "\n - Brine" +
                    "\n - Ethanol" +
                    "\n - Crude Oil" +
                    "\n - Petroleum" +
                    "\n - Super Coolant" +
                    "\n - Naphtha" +
                    "\n - Visco-Gel" +
                    "\n - Liquid Oxygen" +
                    "\n - Liquid Hydrogen" +
                    "\n - Liquid Carbon Dioxide" +
                    "\n - Nuclear Waste";
            }

            public class GAS
            {
                public static LocString NAME = "Gas Tag";
                public static LocString DESC = "A wall tag for gases.";
                public static LocString EFFECT = "This tag is a visual representation of gases in an industrial environment. Select the type of gas using the menu option. \n\nList of tags: " +
                    "\n - Oxygen" +
                    "\n - Polluted Oxygen" +
                    "\n - Carbon Dioxide" +
                    "\n - Hydrogen" +
                    "\n - Natural Gas" +
                    "\n - Propane" +
                    "\n - Chlorine" +
                    "\n - Helium" +
                    "\n - Sour Gas" +
                    "\n - Steam" +
                    "\n - Nuclear Fallout";
            }

            public class GEYSER
            {
                public static LocString NAME = "Geyser Tag";
                public static LocString DESC = "A wall tag for geysers, vents and volcanos.";
                public static LocString EFFECT = "This tag is a visual representation of geological formations.";
            }

            public class NUMBER
            {
                public static LocString NAME = "Number Tag";
                public static LocString DESC = "A wall tag for numbers.";
                public static LocString EFFECT = "This tag serves to show a single digit.";
            }

            public class LETTER
            {
                public static LocString NAME = "Letter Tag";
                public static LocString DESC = "A wall tag for letters.";
                public static LocString EFFECT = "This tag serves to show a single letter.";
            }
        }

        public class NEXT_ART_BUTTON
        {
            public static LocString TEXT = (LocString)"Next Tag";
            public static LocString TOOLTIP = (LocString)"Changes Tag forward to another of the same tier. {Hotkey}";           
        }

        public class BACK_ART_BUTTON
        {
            public static LocString TEXT = (LocString)"Previous Tag";
            public static LocString TOOLTIP = (LocString)"Changes Tag backwards to another of the same tier. {Hotkey}";
        }

        public class GAS_ELEMENT_TAGS
        {
            public static LocString NOGAS_TAG = (LocString)"Select Gas";

            public static LocString OXYGEN_TAG = (LocString)"Oxygen Gas";
            public static LocString POXYGEN_TAG = (LocString)"Polluted Oxygen";
            public static LocString CO2_TAG = (LocString)"Carbon Dioxide Gas";
            public static LocString HYDROGEN_TAG = (LocString)"Hydrogen Gas";
            public static LocString METHANE_TAG = (LocString)"Natural Gas";
            public static LocString PROPANE_TAG = (LocString)"Propane Gas";
            public static LocString CHLORINE_TAG = (LocString)"Chlorine Gas";
            public static LocString HELIUM_TAG = (LocString)"Helium Gas";
            public static LocString SOUR_TAG = (LocString)"Sour Gas";
            public static LocString STEAM_TAG = (LocString)"Steam";
            public static LocString FALLOUT_TAG = (LocString)"Nuclear Fallout";
        }

        public class LIQUID_ELEMENT_TAGS
        {
            public static LocString NOLIQUID_TAG = (LocString)"Select Liquid";

            public static LocString WATER_TAG = (LocString)"Water";
            public static LocString POLLUTEDWATER_TAG = (LocString)"Polluted Water";
            public static LocString SALTWATER_TAG = (LocString)"Salt Water";
            public static LocString BRINE_TAG = (LocString)"Brine";
            public static LocString ETHANOL_TAG = (LocString)"Ethanol";
            public static LocString CRUDEOIL_TAG = (LocString)"Crude Oil";
            public static LocString PETROLEUM_TAG = (LocString)"Petroleum";
            public static LocString COOL_TAG = (LocString)"Super Coolant";
            public static LocString NAPHTHA_TAG = (LocString)"Naphtha";
            public static LocString VISCO_TAG = (LocString)"Visco-Gel";
            public static LocString LIQUID_02_TAG = (LocString)"Liquid Oxygen";
            public static LocString LIQUID_H_TAG = (LocString)"Liquid Hydrogen";
            public static LocString LIQUID_CO2_TAG = (LocString)"Liquid Carbon Dioxide";
            public static LocString NWASTE_TAG = (LocString)"Nuclear Waste";

        }

        public class SOLID_ELEMENT_TAGS
        {
            public static LocString NOSOLID_TAG = "Select Solid";
            public static LocString AGRICULTURE_TAG = "Agriculture";
            public static LocString CONSUMORE_TAG = "Consumable Ore";
            public static LocString CRITTEREGG_TAG = "Critter Egg";
            public static LocString CULSOIL_TAG = "Cultivable Soil";
            public static LocString FILTRATION_TAG = "Filtration Medium";
            public static LocString INDING_TAG = "Industrial Ingredient";
            public static LocString LIQUEFIABLE_TAG = "Liquefiable";
            public static LocString MANUFACT_TAG = "Manufactured Material";
            public static LocString MEDSUP_TAG = "Medical Supplies";
            public static LocString METALORE_TAG = "Metal Ore";
            public static LocString MISC_TAG = "Miscellaneous";
            public static LocString ORGANIC_TAG = "Organic";
            public static LocString RARE_TAG = "Rare Resource";
            public static LocString RAWMINERAL_TAG = "Raw Mineral";
            public static LocString REFMETAL_TAG = "Refined Metal";
            public static LocString REFMINERAL_TAG = "Refined Mineral";
            public static LocString COOKINGING_TAG = "Cooking Ingredient";
            public static LocString EDIBLES_TAG = "Edibles";
            public static LocString SEEDS_TAG = "Seeds";
            public static LocString COMPOSTABLE_TAG = "Compostables";
        }

        public class ALERT_TAGS
        {
            public static LocString NOSALERT_TAG = (LocString)"Select Alert Tag";

            public static LocString WARNING_TAG = (LocString)"Warning";
            public static LocString HOTSPOT_TAG = (LocString)"Hot Spot";
            public static LocString COLDSPOT_TAG = (LocString)"Cold Spot";
            public static LocString COMET_TAG = (LocString)"Comet Alert";
            public static LocString BIOHAZARD_TAG = (LocString)"Biohazard Alert";
            public static LocString RADIATION_TAG = (LocString)"Radiation Alert";
            public static LocString LIGHT_TAG = (LocString)"Brightness Alert";
            public static LocString ELETRIC_TAG = (LocString)"Electricity Alert";
            public static LocString DANGER_TAG = (LocString)"Danger Alert";
        }

        public class UTILITY_TAGS
        {
            public static LocString NOSUT_TAG = (LocString)"Select Utility Tag";

            public static LocString ARROW_TAG = (LocString)"Direction";
            public static LocString DIAGARROW_TAG = (LocString)"Diagonal Direction";
            public static LocString BOTHARROW_TAG = (LocString)"Both Directions";
            public static LocString GASPUMP_TAG = (LocString)"Gas Pump";
            public static LocString LIQUIDPUMP_TAG = (LocString)"Liquid Pump";
            public static LocString SOLIDCON_TAG = (LocString)"Solid Conveyor";
        }

        public class SYSTEM_TAGS
        {
            public static LocString NOSYS_TAG = (LocString)"Select System Tag";
            public static LocString CLOSEDCY_TAG = (LocString)"Closed Cycle";
            public static LocString ORGANICCY_TAG = (LocString)"Organic Recycle";
            public static LocString RECYCLE_TAG = (LocString)"Recycle";
            public static LocString POWER_TAG = (LocString)"Power Toggle";
            public static LocString GASSEN_TAG = (LocString)"Gas Element Sensor";
            public static LocString LIQUIDSEN_TAG = (LocString)"Liquid Element Sensor";
            public static LocString MOTIONSEN_TAG = (LocString)"Motion Sensor";
            public static LocString POWERSEN_TAG = (LocString)"Power Sensor";
            public static LocString PRESSURESEN_TAG = (LocString)"Pressure Sensor";
            public static LocString TEMPSEN_TAG = (LocString)"Temperature Sensor";
            public static LocString TIMESEN_TAG = (LocString)"Time Sensor";

        }

        public class INFO_TAGS
        {
            public static LocString NOINFO_TAG = (LocString)"Select Info Tag";
            public static LocString ATMO_TAG = (LocString)"Atmo Suit";
            public static LocString JET_TAG = (LocString)"Jet Suit";
            public static LocString LEADSUIT_TAG = (LocString)"Lead Suit";
            public static LocString DORMITORY_TAG = (LocString)"Dormitory";
            public static LocString INFIRMARY_TAG = (LocString)"Infirmary";
            public static LocString WORKSHOP_TAG = (LocString)"Workshop";
            public static LocString RESEARCH_TAG = (LocString)"Research Facility";
            public static LocString OBSERVATORY_TAG = (LocString)"Observatory";
            public static LocString RESTROOM_TAG = (LocString)"Restroom";
            public static LocString FOOD_TAG = (LocString)"Refectory";
            public static LocString FARM_TAG = (LocString)"Farm";
            public static LocString CRITTERRANCH_TAG = (LocString)"Critter Ranch";
            public static LocString ROCKET_TAG = (LocString)"Rocket Platform";
        }

        public class NUMBER_TAGS
        {
            public static LocString NONUMBER_TAG = "Numbers Tag";
            public static LocString ONE_TAG = "1";
            public static LocString TWO_TAG = "2";
            public static LocString THREE_TAG = "3";
            public static LocString FOUR_TAG = "4";
            public static LocString FIVE_TAG = "5";
            public static LocString SIX_TAG = "6";
            public static LocString SEVEN_TAG = "7";
            public static LocString EIGHT_TAG = "8";
            public static LocString NINE_TAG = "9";
            public static LocString ZERO_TAG = "0";
        }

        public class LETTER_TAGS
        {
            public static LocString NOLETTER_TAG = "Letters Tag";
            public static LocString A_TAG = "A";
            public static LocString B_TAG = "B";
            public static LocString C_TAG = "C";
            public static LocString D_TAG = "D";
            public static LocString E_TAG = "E";
            public static LocString F_TAG = "F";
            public static LocString G_TAG = "G";
            public static LocString H_TAG = "H";
            public static LocString I_TAG = "I";
            public static LocString J_TAG = "J";
            public static LocString K_TAG = "K";
            public static LocString L_TAG = "L";
            public static LocString M_TAG = "M";
            public static LocString N_TAG = "N";
            public static LocString O_TAG = "O";
            public static LocString P_TAG = "P";
            public static LocString Q_TAG = "Q";
            public static LocString R_TAG = "R";
            public static LocString S_TAG = "S";
            public static LocString T_TAG = "T";
            public static LocString U_TAG = "U";
            public static LocString V_TAG = "V";
            public static LocString W_TAG = "W";
            public static LocString X_TAG = "X";
            public static LocString Y_TAG = "Y";
            public static LocString Z_TAG = "Z";
        }
    }
}
