using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitPursuingState : UnitMoveState, IUnitPursuingState {
        public override void Update() {
            if (target.IsUnvailableForAttack())
                return;
            currentUnit.GetNavUnit().SetDestination(target.GetPosition());
            base.Update();
        }

        protected override void OnTargetReached() {
            
        }
    }
}