using Assets.src.battle;

namespace Assets.src.models {
    public class UnitModel : IModel, ITarget {
        public void SetDamage(int damage) {
            
        }

        public bool IsDefender { get { return false; } }
    }
}