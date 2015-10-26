using Assets.src.battle;
using Assets.src.models;
using Assets.src.services;
using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class UnitAttackState : BaseUnitState, IUnitAttackState {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        protected ICooldownItem attackSpeedCooldown;

        protected ITarget target;

        protected Weapon weapon;

        public void SetTarget(ITarget targetParam) {
            target = targetParam;
        }

        public void SetWeapon(Weapon weaponParam) {
            weapon = weaponParam;
        }

        protected bool CheckAttackPossibility() {
            return !currentUnit.IsUnvailableForAttack() && !target.IsUnvailableForAttack() && currentUnit.CheckAttackDistance(target);
        }

        public override void Start() {
            if (!CheckAttackPossibility())
                Stop();
            Shoot();
        }

        protected void Shoot() {
            if (target.IsUnvailableForAttack() || currentUnit.IsUnvailableForAttack())
                return;
            weapon.ShootTo(currentUnit.GetPosition(), target);
            attackSpeedCooldown = CooldownService.AddCooldown(1f/currentUnit.GetUnitData().attackSpeed, null, Start, 0, 0.1f);
        }

        public override void Update() {
            
        }

        public override void ForceStop() {
            CooldownService.RemoveCooldown(attackSpeedCooldown);
        }
    }
}