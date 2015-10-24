using Assets.src.battle;

namespace Assets.src.models {
    public interface ITargetSelector {
        void SetProvider(ITargetProvider provider);
        void SetCurrentUnit(ITarget currentUnitParam);
        ITarget FindTarget();
    }
}