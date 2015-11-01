using System;
using Assets.Common.Extensions;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.data {
    [Serializable]
    public class SpawnerData {
        public UnitTypes produceUnitType;
        public int level;
        public BarracksUpgradeData[] upgradeData;
    }
}