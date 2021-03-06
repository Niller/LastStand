using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.managers;
using Assets.src.utils;
using Assets.src.views;
using ru.pragmatix.orbix.world.units;
using strange.extensions.injector.api;
using strange.extensions.pool.api;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.src.models {
    public abstract partial class BaseUnitModel<TTargetSelector, TTargetProvider> : IUnit
        where TTargetSelector : ITargetSelector
        where TTargetProvider : ITargetProvider {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        [Inject]
        public IHUDManager HUDManager { get; set; }

        [Inject]
        public IViewModelManager ViewModelManager { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        protected BaseUnitView unitView;

        protected ITargetBehaviour targetBehaviour;

        protected TTargetSelector targetSelector;

        protected TTargetProvider targetProvider;

        protected UnitData data;

        protected INavigationUnit navigationUnit;

        protected ITarget currentTarget;

        protected ITarget priorityTarget;

        protected UnitTypes type;

        protected ISelectableBehaviour selectableBehaviour;

        protected Dictionary<Type, IBuff> buffs = new Dictionary<Type, IBuff>();

        protected IAnimatorHelper animatorHelper;

        public void Spawn(Vector3 position, UnitData dataParam, UnitTypes typeParam, bool isDefenderParam) {
            data = dataParam;
            type = typeParam;
            targetBehaviour = new UnitTargetBehaviour();
            InjectionBinder.injector.Inject(targetBehaviour);
            targetBehaviour.Initialize(data, isDefenderParam, this);
            targetBehaviour.OnDestroyed += Destroy;
            InitView(position);
            animatorHelper = new AnimatorHelper(unitView.animator);
            Initialize();
        }

        public int GetXPReward() {
            return data.xpPrice;
        }

        public int GetGoldReward() {
            return data.goldPrice;
        }

        protected void InitView(Vector3 position) {
            var prefab = GetViewPrefab();
            var unitGO = Object.Instantiate(prefab);
            unitGO.SetActive(true);
            unitGO.transform.position = position;
            unitGO.transform.parent = GameObject.Find("game").transform;
            SetView(unitGO.GetComponent<IView>());
            GetView().SetModel(this);
            SetNavUnit(unitView);
           
        }

        protected abstract GameObject GetViewPrefab();

        protected virtual void Initialize() {
            targetProvider = Activator.CreateInstance<TTargetProvider>();
            InjectionBinder.injector.Inject(targetProvider);
            targetProvider.SetCurrentUnit(this);
            targetSelector = Activator.CreateInstance<TTargetSelector>();
            InjectionBinder.injector.Inject(targetSelector);
            targetSelector.SetProvider(targetProvider);
            targetSelector.SetCurrentUnit(this);
            selectableBehaviour = unitView.SelectableBehaviour;
            if (selectableBehaviour != null) {
                selectableBehaviour.OnSelected += OnSelected;
                selectableBehaviour.OnDeselected += OnDeselected;
            }
        }

        public void SetView(IView view) {
            unitView = view as BaseUnitView;
            unitView.OnUpdate += Update;
        }

        public IView GetView() {
            return unitView;
        }

        public ITargetBehaviour GetTargetBehaviour() {
            return targetBehaviour;
        }

        public virtual void DoDamage(ITarget target, int damage) {
            target.GetTargetBehaviour().SetDamage(damage);
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
            bool checkDistance =  Vector3.Distance(target.GetTargetBehaviour().GetPosition(), GetView().GetPosition()) - target.GetTargetBehaviour().GetVulnerabilityRadius() <=
                   GetUnitData().attackRange;
            return checkDistance;
        }

        protected bool FindTarget() {
            if (priorityTarget != null) {
                priorityTarget = priorityTarget.GetTargetBehaviour().IsUnvailableForAttack() ? null : priorityTarget;
            }
            
            if (currentTarget != null && selectableBehaviour != null && selectableBehaviour.IsSelected()) {
                if (!currentTarget.GetTargetBehaviour().IsUnvailableForAttack())
                    OnDeselected();
            }
            
            currentTarget = priorityTarget ?? targetSelector.FindTarget();
            
            if (selectableBehaviour != null && unitView.SelectableBehaviour.IsSelected()) {
                OnSelected();
            }
            
            return currentTarget != null;
        }

        public void SetPriorityTarget(ITarget target, bool isOverride) {
            if (isOverride || priorityTarget == null) {
                priorityTarget = target;
                ForceStopCurrentState();
                StartPursueOrIdle();
            }
        }

        public void SetMovePoint(ITarget target) {
            ForceStopCurrentState();
            OnDeselected();
            currentTarget = target;
            priorityTarget = target;
            EnterMoveState();
        }

        public ITarget GetCurrentTarget() {
            return currentTarget;
        }

        public abstract bool IsManualControl { get; }

        private void OnDeselected() {
            if (currentTarget != null && !currentTarget.GetTargetBehaviour().IsUnvailableForAttack() && !(currentTarget is TempTarget))
                HUDManager.RemoveHUD(((IModel)currentTarget).GetView().GetGameObject(), HudTypes.TARGET_POINTER);
        }

        private void OnSelected() {
            if (currentTarget != null && !currentTarget.GetTargetBehaviour().IsUnvailableForAttack() && !(currentTarget is TempTarget))
                HUDManager.AddHUD(((IModel)currentTarget).GetView().GetGameObject(), HudTypes.TARGET_POINTER);
        }

        public void AddBuff<T>(T buff) where T : IBuff {
            if (buffs.ContainsKey(typeof(T))) {
                RemoveBuff<T>();
            }
            buff.Apply(this);
            buffs.Add(typeof(T), buff);
            buff.OnEnd += RemoveBuff;
        }

        protected void RemoveBuff(IBuff buff) {
            buffs.Remove(buff.GetType());
        }

        public void RemoveBuff<T>() where T : IBuff {
            var buff = buffs[typeof (T)];
            buffs.Remove(typeof(T));
            buff.Free();
            buff.OnEnd = null;
        }

        protected void ClearBuffs() {
            var temp = buffs.Values.ToList();
            foreach (var buff in temp) {
                RemoveBuff(buff);
            }
            buffs.Clear();
        }

        public float GetMovementSpeed() {
            return GetUnitData().movementSpeed;
        }

        public float GetAttackSpeed() {
            return GetUnitData().attackSpeed;
        }

    }

    public abstract partial class BaseUnitModel<TTargetSelector, TTargetProvider> : IUnit
        where TTargetSelector : ITargetSelector
        where TTargetProvider : ITargetProvider {
        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

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
            attackState.SetWeapon(new Weapon(GetUnitData().damage, InjectionBinder.GetInstance<IPool<GameObject>>(GameDataService.GetBulletType(type)), this));
            attackState.Initialize(this, animatorHelper);

        }

        protected virtual void InitPursuingState() {
            pursuingState = new UnitPursuingState();
            InjectionBinder.injector.Inject(pursuingState);
            pursuingState.Initialize(this, animatorHelper);
        }

        protected virtual void InitIdleState() {
            idleState = new UnitIdleState();
            InjectionBinder.injector.Inject(idleState);
            idleState.Initialize(this, animatorHelper);
        }

        protected virtual void InitMoveState() {
            moveState = new UnitMoveState();
            InjectionBinder.injector.Inject(moveState);
            moveState.Initialize(this, animatorHelper);
        }

        protected virtual void InitDieState() {
            dieState = new UnitDieState();
            InjectionBinder.injector.Inject(dieState);
            dieState.Initialize(this, animatorHelper);
        }

        protected void OnCurrentStateStopped() {
            //���������, �� ���������� �� ��� �������
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
            priorityTarget = null;
            StartPursueOrIdle();
        }

        private void TransitAfterPursuingState() {
            TryAttack();
        }

        private void TransitAfterIdleState() {
            StartPursueOrIdle();
        }

        private void TransitAfterDieState() {
            GetView().Destroy();
        }

        protected void TryAttack() {
            if (!currentTarget.GetTargetBehaviour().IsUnvailableForAttack()) {
                EnterAttackState();
            } else {
                StartPursueOrIdle();
            }
        }

        protected virtual void EnterAttackState() {
            
            currentState = attackState;
            currentState.OnStop += OnCurrentStateStopped;
            attackState.SetTarget(currentTarget);
            currentState.Start();
            var unit = currentTarget as IUnit;
            if (unit != null) {
                unit.SetPriorityTarget(this, false);
            }
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

        protected virtual void EnterMoveState() {
            currentState = moveState;
            currentState.OnStop += OnCurrentStateStopped;
            moveState.SetTarget(currentTarget);
            currentState.Start();
        }

        protected virtual void EnterDieState() {
            currentState = dieState;
            currentState.OnStop += OnCurrentStateStopped;
            currentState.Start();
        }

        protected void Destroy() {
            ClearBuffs();
            SelectionManager.Deselect(selectableBehaviour);
            ForceStopCurrentState();
            EnterDieState();
            
        }

        public virtual void Update() {
            stateChangesInThisFrame = 0;
            if (currentState != null)
                currentState.Update();
            if (priorityTarget == null && currentState is UnitMoveState && !(currentState is IUnitPursuingState)) {
                if (FindTarget()) {
                    ForceStopCurrentState();
                    StartPursueOrIdle();
                }
            }
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
                if (GetTargetBehaviour().IsDefender) {
                    if (BattleManager.GetFontain().CheckInFontaionRadius(GetView().GetPosition())) {
                        EnterIdleState();
                    } else {
                        currentTarget = new TempTarget(BattleManager.GetFontain().GetView().GetPosition());
                        EnterMoveState();
                        priorityTarget = null;
                    }
                } else {
                    EnterIdleState();
                }
            }
            
        }

    }
}