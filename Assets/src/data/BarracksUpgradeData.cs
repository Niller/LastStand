using System;

namespace Assets.src.data {
    [Serializable]
    public class BarracksUpgradeData {
        public int cost;
        public int trainingSpeed;
        public UnitData unitDeltaStats;
    }
}