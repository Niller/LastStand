using Assets.src.battle;
using Assets.src.models;

namespace ru.pragmatix.orbix.world.units {
    public interface IUnitAttackState : IUnitState {
        void SetTarget(ITarget targetParam);
        void SetWeapon(Weapon weaponParam);
    }
}