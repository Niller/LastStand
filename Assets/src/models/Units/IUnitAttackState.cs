using Assets.src.battle;

namespace ru.pragmatix.orbix.world.units {
    public interface IUnitAttackState : IUnitState {
        void SetTarget(ITarget targetParam);
    }
}