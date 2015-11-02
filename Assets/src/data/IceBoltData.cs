using System;

namespace Assets.src.data {
    [Serializable]
    public class IceBoltData : SpellData {
        public int damage;
        public FreezeBuffData buffData;
    }
}