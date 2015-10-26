using System;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.mediators;
using Assets.src.views;
using ru.pragmatix.orbix.world.units;
using strange.extensions.injector.api;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.src.models {
    public abstract partial class BaseUnitModel<TTargetSelector,TTargetProvider> : BaseTargetModel, IUnit 
        where TTargetSelector : ITargetSelector 
        where TTargetProvider : ITargetProvider {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        protected TTargetSelector targetSelector;

        protected TTargetProvider targetProvider;

        public UnitMediator Mediator { protected get; set; }

        protected UnitData data;

        protected INavigationUnit navigationUnit;

        public ITarget currentTarget;

        protected ITarget priorityTarget;

        protected UnitInformer unitInformer;

        public override void Initialize(BaseBattleInformer informerParam) {
            base.Initialize(informerParam);
            targetProvider = Activator.CreateInstance<TTargetProvider>();
            InjectionBinder.injector.Inject(targetProvider);
            targetProvider.SetCurrentUnit(this);
            targetSelector = Activator.CreateInstance<TTargetSelector>();
            InjectionBinder.injector.Inject(targetSelector);
            targetSelector.SetProvider(targetProvider);
            targetSelector.SetCurrentUnit(this);
        }

        protected override void InitializeData() {       
            base.InitializeData();
            unitInformer = informer as UnitInformer;
            data = unitInformer.data;
            if (data != null) {
                currentHealth = data.health;                     
            } else {
                Debug.LogError("Data isn't valid for this object", Mediator);
            }
        }

        public void SetNavUnit(INavigationUnit navUnit) {
            navigationUnit = navUnit;
        }

        public INavigationUnit GetNavUnit() {
            return navigationUnit;
        }

        public UnitData GetUnitData() {
            return data;
        }

        public bool CheckAttackDistance(ITarget target) {
            return Vector3.Distance(target.GetPosition(), GetPosition())-target.GetVulnerabilityRadius() <=
                   GetUnitData().attackRange;
        }

        protected bool FindTarget() {
            if (priorityTarget != null) {
                priorityTarget = priorityTarget.IsUnvailableForAttack() ? null : priorityTarget;
            }
            currentTarget = priorityTarget ?? targetSelector.FindTarget();
            return currentTarget != null;
        }

        public void SetPriorityTarget(ITarget target, bool isOverride) {
            if (isOverride || priorityTarget == null) {
                priorityTarget = target;
                ForceStopCurrentState();
                StartPursueOrIdle();
            }
        }
    }

    public abstract partial class BaseUnitModel<TTargetSelector, TTargetProvider> : BaseTargetModel
        where TTargetSelector : ITargetSelector
        where TTargetProvider : ITargetProvider {

        [Inject]
        public IGameDataService GameDataService { get; set; }

        protected IUnitPursuingState pursuingState;

        protected IUnitMoveState moveState;

        protected IUnitAttackState attackState;

        protected IUnitIdleState idleState;

        protected IUnitDieState dieState;

        protected IUnitState currentState;

        protected int stateChangesInThisFrame;

        public virtual void InitializeStates() {
            InitPursuingState();
            InitIdleState();
            InitMoveState();
            InitAttackState();
            InitDieState();
        }

        public virtual void StartAct() {
            StartPursueOrIdle();
        }

        protected virtual void InitAttackState() {
            attackState = new UnitAttackState();
            InjectionBinder.injector.Inject(attackState);
            attackState.SetWeapon(new Weapon(GetUnitData().damage, InjectionBinder.GetInstance<IPool<GameObject>>(GameDataService.GetBulletType(unitInformer.type))));
            attackState.Initialize(this, null);
            
        }

        protected virtual void InitPursuingState() {
            pursuingState = new UnitPursuingState();
            InjectionBinder.injector.Inject(pursuingState);
            pursuingState.Initialize(this, null);
        }

        protected virtual void InitIdleState() {
            idleState = new UnitIdleState();
            InjectionBinder.injector.Inject(idleState);
            idleState.Initialize(this, null);
        }

        protected virtual void InitMoveState() {
            moveState = new UnitMoveState();
            InjectionBinder.injector.Inject(moveState);
            moveState.Initialize(this, null);
        }

        protected virtual void InitDieState() {
            dieState = new UnitDieState();
            InjectionBinder.injector.Inject(dieState);
            dieState.Initialize(this, null);
        }

        protected void OnCurrentStateStopped() {
            //ѕровер€ем, не зациклилс€ ли наш автомат
            stateChangesInThisFrame++;
            if (stateChangesInThisFrame > 10) {
                currentState = null;
                Debug.LogError("We've stucked in infinite loop :-(");
            }

            //if (AttackManager.IsBattleEnd)
            //    return;
            currentState.OnStop -= OnCurrentStateStopped;
            OnStateStopped();
        }

        protected virtual void OnStateStopped() {
            if (currentState is IUnitPursuingState) {
                TransitAfterPursuingState();
                return;
            }
            if (currentState is IUnitMoveState) {
                TransitAfterMoveState();
                return;
            }
            if (currentState is IUnitIdleState) {
                TransitAfterIdleState();
                return;
            }
            
            if (currentState is IUnitAttackState) {
                TransitAfterAttackState();
                return;
            }

            if (currentState is IUnitDieState) {
                TransitAfterDieState();
                return;
            }
            
            currentState = null;
        }

        private void TransitAfterAttackState() {
            StartPursueOrIdle();
        }

        private void TransitAfterMoveState() {
            StartPursueOrIdle();
        }

        private void TransitAfterPursuingState() {
            EnterAttackState();
        }   

        private void TransitAfterIdleState() {
            StartPursueOrIdle();
        }

        private void TransitAfterDieState() {
            
        }

        protected virtual void EnterAttackState() {
            currentState = attackState;
            currentState.OnStop += OnCurrentStateStopped;
            attackState.SetTarget(currentTarget);
            currentState.Start();
        }

        protected virtual void EnterIdleState() {
            currentState = idleState;
            currentState.OnStop += OnCurrentStateStopped;
            currentState.Start();
        }

        protected virtual void EnterPursuingState() {
            currentState = pursuingState;
            currentState.OnStop += OnCurrentStateStopped;
            pursuingState.SetTarget(currentTarget);
            currentState.Start();
        }

        protected virtual void EnterDieState() {
            currentState = dieState;
            currentState.OnStop += OnCurrentStateStopped;
            currentState.Start();
        }

        protected override void Destroy() {
            ForceStopCurrentState();
            EnterDieState();
            base.Destroy();
        }

        public virtual void Update() {
            stateChangesInThisFrame = 0;
            if (currentState != null)
                currentState.Update();
        }

        protected virtual void ForceStopCurrentState() {
            if (currentState != null) {
                currentState.OnStop -= OnCurrentStateStopped;
                currentState.ForceStop();
                currentState = null;
            }
        }

        protected void StartPursueOrIdle() {
            if (FindTarget()) {
                EnterPursuingState();
            } else {
                EnterIdleState();
            }
        }

    }

    public class UnitModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {
        
    }
}