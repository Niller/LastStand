using Assets.src.battle;

namespace ru.pragmatix.orbix.world.units {
    public class UnitMoveState : BaseUnitState, IUnitMoveState {

        protected ITarget target;

        public void SetTarget(ITarget targetParam) {
            target = targetParam;
        }

        public override void Start() {
            currentUnit.GetNavUnit().SetDestination(target.GetPosition());
        }

        public override void Update() {
            if (target.IsUnvailableForAttack())
                return;
            if (currentUnit.CheckAttackDistance(target)) {
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