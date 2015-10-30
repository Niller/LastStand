using Assets.src.battle;
using Assets.src.models;
using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitMoveState : BaseUnitState, IUnitMoveState {

        protected ITarget target;

        public void SetTarget(ITarget targetParam) {
            target = targetParam;
        }

        public override void Start() {
            currentUnit.GetNavUnit().SetDestination(target.GetTargetBehaviour().GetPosition());
        }

        public override void Update() {
            if (target.GetTargetBehaviour().IsUnvailableForAttack()) 
                return;
            if (Vector3.Distance(currentUnit.GetTargetBehaviour().GetPosition(), target.GetTargetBehaviour().GetPosition()) < target.GetTargetBehaviour().GetVulnerabilityRadius()) {
                Stop();
            }
        }

        public override void ForceStop() {
            currentUnit.GetNavUnit().StopMoving();
        }

        protected virtual void OnTargetReached() {
            Stop();
        }
    }
}