using System;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.data {
    [Serializable]
    public class SpawnerData {
        public UnitTypes produceUnitType;
        public int level;
        public int waveSpanwLimit;
        public BarracksUpgradeData[] upgradeData;
    }
}