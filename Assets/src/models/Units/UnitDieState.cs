using System;
using Assets.src.services;

namespace ru.pragmatix.orbix.world.units {
    public class UnitDieState : BaseUnitState, IUnitDieState {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        public override void Start() {
            animatorHelper.SetAnimatorBool("dying", true);
            CooldownService.AddCooldown(1f, null, Die, 0, 0.5f);
        }

        private void Die() {
            Stop();
        }

        public override void Update() {
        }

        public override void ForceStop() {
        }
    }
}