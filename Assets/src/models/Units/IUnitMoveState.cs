using Assets.src.battle;
using Assets.src.models;

namespace ru.pragmatix.orbix.world.units {
    public interface IUnitMoveState : IUnitState {
        void SetTarget(ITarget targetParam);
    }
}