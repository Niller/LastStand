using Assets.src.battle;
using Assets.src.models;
using Assets.src.services;
using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitAttackState : BaseUnitState, IUnitAttackState {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        protected ICooldownItem attackSpeedCooldown;

        protected ICooldownItem attackCooldown;

        protected ITarget target;

        protected Weapon weapon;

        public override void Initialize(IUnit unit, IAnimatorHelper animatorHelperParam) {
            base.Initialize(unit, animatorHelperParam);
        }

        public void SetTarget(ITarget targetParam) {
            target = targetParam;
        }

        public void SetWeapon(Weapon weaponParam) {
            weapon = weaponParam;
        }

        protected bool CheckAttackPossibility() {
            return !currentUnit.GetTargetBehaviour().IsUnvailableForAttack() && !target.GetTargetBehaviour().IsUnvailableForAttack() && currentUnit.CheckAttackDistance(target);
        }

        public override void Start() {
            StartAttackAnimation();
        }

        protected void StartAttack() {
            if (!CheckAttackPossibility()) {
                Stop();
                return;
            }
            StartAttackAnimation();
        }

        private void Attack() {
            if (!currentUnit.GetTargetBehaviour().IsUnvailableForAttack() &&
                !target.GetTargetBehaviour().IsUnvailableForAttack()) {
                Shoot();
            } else {
                Stop();
            }
        }

        protected void StartAttackAnimation() {
            currentUnit.GetNavUnit().RotateToPosition(target.GetTargetBehaviour().GetPosition());
            animatorHelper.PlayAnimation("attacking");
            attackCooldown = CooldownService.AddCooldown(0.5f, null, Attack, 0, 0.1f);
        }

        protected void Shoot() {
            if (target.GetTargetBehaviour().IsUnvailableForAttack() ||
                currentUnit.GetTargetBehaviour().IsUnvailableForAttack()) {
                Stop();
                return;
            }
            weapon.ShootTo(currentUnit.GetTargetBehaviour().GetPosition(), target);
            attackSpeedCooldown = CooldownService.AddCooldown(1f/currentUnit.GetUnitData().attackSpeed, null, StartAttack, 0, 0.1f);
        }

        public override void Update() {
            
        }

        public override void ForceStop() {
            animatorHelper.SetAnimatorBool("attacking", false);
            CooldownService.RemoveCooldown(attackSpeedCooldown);
            CooldownService.RemoveCooldown(attackCooldown);
        }
    }
}