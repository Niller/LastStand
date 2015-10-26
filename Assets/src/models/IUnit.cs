using Assets.src.battle;
using Assets.src.data;
using Assets.src.views;

namespace Assets.src.models {
    public interface IUnit : ITarget {
        INavigationUnit GetNavUnit();
        UnitData GetUnitData();
        bool CheckAttackDistance(ITarget target);
        void InitializeStates();
        void StartAct();
        void SetPriorityTarget(ITarget target, bool isOverride);
    }
}