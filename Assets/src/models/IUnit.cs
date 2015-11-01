using Assets.src.battle;
using Assets.src.data;
using Assets.src.utils;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public interface IUnit : IAttackableTarget, ITargetWithReward {
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
        void Spawn(Vector3 position, UnitData dataParam, UnitTypes typeParam, bool isDefenderParam);
        
    }
}