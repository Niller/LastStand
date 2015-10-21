using System.Collections.Generic;

namespace Assets.src.battle {
    public class BattleManager : IBattleManager {

        private List<ITarget> attackers;

        private List<ITarget> defenders;

        private List<ITarget> GetAppropriateListForTarget(ITarget target) {
            return target.IsDefender ? defenders : attackers;
        }

        public void Initialize() {
            attackers = new List<ITarget>();
            defenders = new List<ITarget>();
        }

        public void RegisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Add(target);
        }

        public void UnregisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Remove(target);
        }
    }
}