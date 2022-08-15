using Database;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class GermIdx
    {
        public const byte Invalid = byte.MaxValue;

        private static byte _foodPoisoning = Invalid;
        public static byte FoodPoisoningIdx
        {
            get
            {
                if (_foodPoisoning == Invalid)
                    _foodPoisoning = Db.Get().Diseases.GetIndex(Db.Get().Diseases.FoodGerms.id);
                return _foodPoisoning;
            }
        }

        private static byte _pollen = Invalid;
        public static byte PollenGermsIdx
        {
            get
            {
                if (_pollen == Invalid)
                    _pollen = Db.Get().Diseases.GetIndex(Db.Get().Diseases.PollenGerms.id);
                return _pollen;
            }
        }

        private static byte _slimelung = Invalid;
        public static byte SlimelungIdx
        {
            get
            {
                if (_slimelung == Invalid)
                    _slimelung = Db.Get().Diseases.GetIndex(Db.Get().Diseases.SlimeGerms.id);
                return _slimelung;
            }
        }

        private static byte _zombieSpores = Invalid;
        public static byte ZombieSporesIdx
        {
            get
            {
                if (_zombieSpores == Invalid)
                    _zombieSpores = Db.Get().Diseases.GetIndex(Db.Get().Diseases.ZombieSpores.id);
                return _zombieSpores;
            }
        }

        private static byte _radPoisoning = Invalid;
        public static byte RadiationPoisoningIdx
        {
            get
            {
                if (_radPoisoning == Invalid && DlcManager.IsExpansion1Active())
                    _radPoisoning = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.id);
                return _radPoisoning;
            }
        }

        private static byte _hungerms = Invalid;
        public static byte HungerGermsIdx
        {
            get
            {
                if (_hungerms == Invalid)
                    _hungerms = Db.Get().Diseases.GetIndex(HungerGerms.ID);
                return _hungerms;
            }
        }

        private static byte _bogInsects = Invalid;
        public static byte BogInsectsIdx
        {
            get
            {
                if (_bogInsects == Invalid && DlcManager.IsExpansion1Active())
                    _bogInsects = Db.Get().Diseases.GetIndex(BogInsects.ID);
                return _bogInsects;
            }
        }

        private static byte _frostShards = Invalid;
        public static byte FrostShardsIdx
        {
            get
            {
                if (_frostShards == Invalid)
                    _frostShards = Db.Get().Diseases.GetIndex(FrostShards.ID);
                return _frostShards;
            }
        }

        private static byte _gassyGerms = Invalid;
        public static byte GassyGermsIdx
        {
            get
            {
                if (_gassyGerms == Invalid)
                    _gassyGerms = Db.Get().Diseases.GetIndex(GassyGerms.ID);
                return _gassyGerms;
            }
        }

        private static byte _alienGerms = Invalid;
        public static byte AlienGermsIdx
        {
            get
            {
                if (_alienGerms == Invalid)
                    _alienGerms = Db.Get().Diseases.GetIndex(AlienGerms.ID);
                return _alienGerms;
            }
        }

        private static byte _mutatingGerms = Invalid;
        public static byte MutatingGermsIdx
        {
            get
            {
                if (_mutatingGerms == Invalid)
                    _mutatingGerms = Db.Get().Diseases.GetIndex(MutatingGerms.ID);
                return _mutatingGerms;
            }
        }

        private static Dictionary<byte, string> GermNames = new Dictionary<byte, string>(); 

        public static string GetGermName(byte idx)
        {
            if (!GermNames.ContainsKey(idx) && Db.Get().Diseases.Count >= idx)
                GermNames.Add(idx, Db.Get().Diseases[idx].Name);
            
            if (GermNames.ContainsKey(idx))
                return GermNames[idx];

            return string.Empty;
        }
    }
}
