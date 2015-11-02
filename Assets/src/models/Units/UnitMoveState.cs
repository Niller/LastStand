using Assets.src.battle;
using Assets.src.models;
using Assets.src.views;
using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitMoveState : BaseUnitState, IUnitMoveState {

        protected ITarget target;

        public void SetTarget(ITarget targetParam) {
            target = targetParam;
        }

        public override void Start() {
            animatorHelper.SetAnimatorBool("running", true);
            currentUnit.GetNavUnit().SetDestination(target.GetTargetBehaviour().GetPosition());
        }

        public override void Update() {
            if (target.GetTargetBehaviour().IsUnvailableForAttack()) 
                return;
            Debug.Log(Vector3.Distance(currentUnit.GetTargetBehaviour().GetPosition(), target.GetTargetBehaviour().GetPosition()));
            if (Vector3.Distance(currentUnit.GetTargetBehaviour().GetPosition(), target.GetTargetBehaviour().GetPosition()) < currentUnit.GetTargetBehaviour().GetVulnerabilityRadius()) {

                Stop();
            }
        }

        public override void ForceStop() {
            animatorHelper.SetAnimatorBool("running", false);
            currentUnit.GetNavUnit().StopMoving();
        }

        protected virtual void OnTargetReached() {
            Stop();
        }
    }
}