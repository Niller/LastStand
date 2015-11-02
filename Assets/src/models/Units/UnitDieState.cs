using System;

namespace ru.pragmatix.orbix.world.units {
    public class UnitDieState : BaseUnitState, IUnitDieState {
        public override void Start() {
            animatorHelper.SetAnimatorBool("dying", true);
            animatorHelper.GetAnimationEventInformer().OnDie += Die;
        }

        private void Die() {
            Stop();
        }

        public override void Update() {
        }

        public override void ForceStop() {
            animatorHelper.GetAnimationEventInformer().OnDie -= Die;
        }
    }
}