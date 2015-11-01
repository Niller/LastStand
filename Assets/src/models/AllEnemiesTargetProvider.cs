using System.Collections.Generic;
using System.Linq;
using Assets.src.battle;

namespace Assets.src.models {
    public class AllEnemiesTargetProvider : ITargetProvider {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected ITarget currentUnit;

        public void SetCurrentUnit(ITarget currentUnitParam) {
            currentUnit = currentUnitParam;
        }

        public List<ITarget> GetTargets() {
            return currentUnit.GetTargetBehaviour().IsDefender ? BattleManager.GetAttackers() : 
                BattleManager.GetDefenders().Where(unit => !BattleManager.GetFontain().CheckInFontaionRadius(unit.GetTargetBehaviour().GetPosition())).ToList();
        }
    }
}