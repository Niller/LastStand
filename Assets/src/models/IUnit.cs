using Assets.src.battle;
using Assets.src.data;
using Assets.src.mediators;
using Assets.src.views;
using JetBrains.Annotations;

namespace Assets.src.models {
    public interface IUnit : ITarget, IModel {
        INavigationUnit GetNavUnit();
        UnitData GetUnitData();
        bool CheckAttackDistance(ITarget target);
        void InitializeStates();
        void StartAct();
        void SetPriorityTarget(ITarget target, bool isOverride);
        void SetMovePoint(ITarget target);
        ITarget GetCurrentTarget();
        bool IsManualControl { get; }
        void SetNavUnit(INavigationUnit navUnit);
        void Update();
    }
}