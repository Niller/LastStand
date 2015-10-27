using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitPursuingState : UnitMoveState, IUnitPursuingState {
        public override void Update() {
            if (target.IsUnvailableForAttack()) {
                Stop();
                return;
            }
            currentUnit.GetNavUnit().SetDestination(target.GetPosition());
            if (currentUnit.CheckAttackDistance(target)) {
                Stop();
            }
        }

        protected override void OnTargetReached() {
            
        }
    }
}