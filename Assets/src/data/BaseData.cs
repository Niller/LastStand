using System;

namespace Assets.src.data {
    [Serializable]
    public abstract class BaseBattleData {
        public int health;
        public float armor;
        public float vulnerabilityRadius;
    }
}