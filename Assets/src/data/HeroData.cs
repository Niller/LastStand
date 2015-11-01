using System;

namespace Assets.src.data {
    [Serializable]
    public class HeroData : UnitData {
        public int level;
        public int xp;
        public int upgradePoints;
        public int[] spellLevels;

        /*
        public static UnitData operator +(HeroData first, HeroData second) {
            UnitData newUnitData = new UnitData {
                health = first.health + second.health,
                armor = first.armor + second.armor,
                damage = first.damage + second.damage,
                attackSpeed = first.attackSpeed + second.attackSpeed,
                movementSpeed = first.movementSpeed + second.movementSpeed,
                attackRange = first.attackRange + second.attackRange,
                vulnerabilityRadius = first.vulnerabilityRadius + second.vulnerabilityRadius,
                goldPrice = first.goldPrice + second.goldPrice,
                xpPrice = first.xpPrice + second.xpPrice
                
            };
            return newUnitData;
        }
        */

        public new HeroData Copy() {
            HeroData newUnitData = new HeroData {
                health = health,
                armor = armor,
                damage = damage,
                attackSpeed = attackSpeed,
                movementSpeed = movementSpeed,
                attackRange = attackRange,
                vulnerabilityRadius = vulnerabilityRadius,
                goldPrice = goldPrice,
                xpPrice = xpPrice,
                xp = xp,
                level = level,
                upgradePoints = upgradePoints,
            };
            newUnitData.spellLevels = (int[])spellLevels.Clone();
            return newUnitData;
        }
    }
}