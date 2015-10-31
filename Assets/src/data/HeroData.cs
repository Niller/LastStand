using System;

namespace Assets.src.data {
    [Serializable]
    public class HeroData : UnitData {
        public float level;
        public float xp;
        public int[] spellLevels;
    }
}