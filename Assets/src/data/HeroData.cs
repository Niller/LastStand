using System;

namespace Assets.src.data {
    [Serializable]
    public class HeroData : UnitData {
        public float level;
        public int[] spellLevels;
    }
}