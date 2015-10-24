using System;

namespace Assets.src.data {
    [Serializable]
    public class UnitData : BaseBattleData {
        public int health;
        public float armor;
        public int damage;
        public float attackSpeed;
        public float movementSpeed;
        public float attackRange;
        public int goldPrice;
        public int xpPrice;

        public static UnitData operator +(UnitData first, UnitData second) {
            UnitData newUnitData = new UnitData {
                health = first.health + second.health,
                armor = first.armor + second.armor,
                damage = first.damage + second.damage,
                attackSpeed = first.attackSpeed + second.attackSpeed,
                movementSpeed = first.movementSpeed + second.movementSpeed,
                attackRange = first.attackRange + second.attackRange,
                goldPrice = first.goldPrice + second.goldPrice,
                xpPrice = first.xpPrice + second.xpPrice
            };
            return newUnitData;
        }

        public UnitData Copy() {
            UnitData newUnitData = new UnitData {
                health = health,
                armor = armor,
                damage = damage,
                attackSpeed = attackSpeed,
                movementSpeed = movementSpeed,
                attackRange = attackRange,
                goldPrice = goldPrice,
                xpPrice = xpPrice
            };
            return newUnitData;
        }
    }
}