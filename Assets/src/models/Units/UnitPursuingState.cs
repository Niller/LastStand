using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitPursuingState : UnitMoveState, IUnitPursuingState {
        public override void Update() {
            if (target.GetTargetBehaviour().IsUnvailableForAttack()) {
                Stop();
                return;
            }
            currentUnit.GetNavUnit().SetDestination(target.GetTargetBehaviour().GetPosition());
            if (currentUnit.CheckAttackDistance(target)) {
                Stop();
            }
        }

        protected override void OnTargetReached() {
            
        }
    }
}