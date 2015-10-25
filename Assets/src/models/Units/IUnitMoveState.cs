using Assets.src.battle;

namespace ru.pragmatix.orbix.world.units {
    public interface IUnitMoveState : IUnitState {
        void SetTarget(ITarget targetParam);
    }
}