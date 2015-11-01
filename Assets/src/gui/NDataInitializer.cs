using System;
using System.Security.Policy;
using Assets.src.battle;
using Assets.src.models;
using strange.extensions.injector.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Assets.src.gui {
    public class GUIRootContext : EZData.Context {
        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

        #region CurrentOneUnit
        public readonly EZData.VariableContext<UnitContext> CurrentOneUnitEzVariableContext = new EZData.VariableContext<UnitContext>(null);
        public UnitContext CurrentOneUnit {
            get { return CurrentOneUnitEzVariableContext.Value; }
            set { CurrentOneUnitEzVariableContext.Value = value; }
        }
        #endregion

        #region CurrentHero
        public readonly EZData.VariableContext<HeroContext> CurrentHeroEzVariableContext = new EZData.VariableContext<HeroContext>(null);
        public HeroContext CurrentHero {
            get { return CurrentHeroEzVariableContext.Value; }
            set { CurrentHeroEzVariableContext.Value = value; }
        }
        #endregion

        #region CurrentSpawner
        public readonly EZData.VariableContext<SpawnerContext> CurrentSpawnerEzVariableContext = new EZData.VariableContext<SpawnerContext>(null);
        public SpawnerContext CurrentSpawner {
            get { return CurrentSpawnerEzVariableContext.Value; }
            set { CurrentSpawnerEzVariableContext.Value = value; }
        }
        #endregion

        #region Property IsOneUnitSelected
        private readonly EZData.Property<bool> _privateIsOneUnitSelectedProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsOneUnitSelectedProperty { get { return _privateIsOneUnitSelectedProperty; } }
        public bool IsOneUnitSelected {
            get { return IsOneUnitSelectedProperty.GetValue(); }
            set { IsOneUnitSelectedProperty.SetValue(value); }
        }
        #endregion

        #region Property IsHeroSelected
        private readonly EZData.Property<bool> _privateIsHeroSelectedProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsHeroSelectedProperty { get { return _privateIsHeroSelectedProperty; } }
        public bool IsHeroSelected {
            get { return IsHeroSelectedProperty.GetValue(); }
            set { IsHeroSelectedProperty.SetValue(value); }
        }
        #endregion

        #region Property IsSpawnerSelected
        private readonly EZData.Property<bool> _privateIsSpawnerSelectedProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsSpawnerSelectedProperty { get { return _privateIsSpawnerSelectedProperty; } }
        public bool IsSpawnerSelected {
            get { return IsSpawnerSelectedProperty.GetValue(); }
            set { IsSpawnerSelectedProperty.SetValue(value); }
        }
        #endregion

        #region Property Gold
        private readonly EZData.Property<int> _privateGoldProperty = new EZData.Property<int>();
        public EZData.Property<int> GoldProperty { get { return _privateGoldProperty; } }
        public int Gold {
            get { return GoldProperty.GetValue(); }
            set { GoldProperty.SetValue(value); }
        }
        #endregion

        #region Property IsHeroRespawn
        private readonly EZData.Property<bool> _privateIsHeroRespawnProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsHeroRespawnProperty { get { return _privateIsHeroRespawnProperty; } }
        public bool IsHeroRespawn {
            get { return IsHeroRespawnProperty.GetValue(); }
            set { IsHeroRespawnProperty.SetValue(value); }
        }
        #endregion

        #region Property HeroRespawnCooldownPercent
        private readonly EZData.Property<float> _privateHeroRespawnCooldownPercentProperty = new EZData.Property<float>();
        public EZData.Property<float> HeroRespawnCooldownPercentProperty { get { return _privateHeroRespawnCooldownPercentProperty; } }
        public float HeroRespawnCooldownPercent {
            get { return HeroRespawnCooldownPercentProperty.GetValue(); }
            set { HeroRespawnCooldownPercentProperty.SetValue(value); }
        }
        #endregion

        public void Initialize() {
            SelectionManager.SelectionChanged += SelectionChanged;
            GameManager.Gold.OnPropertyChanged += OnGoldChanged;
            Gold = GameManager.Gold.Value;
            BattleManager.OnHeroRespawnStart += OnHeroRespawnStart;
            BattleManager.OnHeroRespawnEnd += OnHeroRespawnEnd;
            IsHeroRespawn = false;
        }

        private void OnHeroRespawnEnd() {
            IsHeroRespawn = false;
            if (BattleManager.GetHeroRespawnCooldown() != null)
                BattleManager.GetHeroRespawnCooldown().OnTick -= OnTickHeroRespawnCooldown;
        }

        private void OnHeroRespawnStart() {
            IsHeroRespawn = true;
            BattleManager.GetHeroRespawnCooldown().OnTick += OnTickHeroRespawnCooldown;
        }

        private void OnTickHeroRespawnCooldown() {
            HeroRespawnCooldownPercent =1f- BattleManager.GetHeroRespawnCooldown().GetPCT();
        }

        private void OnGoldChanged(int gold) {
            Gold = gold;
        }

        private void SelectionChanged() {
            IsOneUnitSelected = SelectionManager.GetSelectedObjects().Count == 1;
            if (IsOneUnitSelected) {
                var selectedTarget =
                    SelectionManager.GetSelectedObjects()[0].GetView().GetModel<ITarget>();
                if (selectedTarget != null) {
                    IsSpawnerSelected = false;
                    CurrentSpawner = null;
                    if (selectedTarget is HeroModel) {
                        IsHeroSelected = true;
                        CurrentHero = new HeroContext();
                        //CurrentHero.SetTargetBehaviour(selectedObject.GetTargetBehaviour());
                        InjectionBinder.injector.Inject(CurrentHero);
                        CurrentHero.InitializeSlots(selectedTarget as HeroModel);
                    } else {
                        IsHeroSelected = false;
                        CurrentHero = null;
                    } //else {

                    CurrentOneUnit = new UnitContext();
                    CurrentOneUnit.SetTargetBehaviour(selectedTarget.GetTargetBehaviour());
                } else {
                    var selectedSpawner =
                        SelectionManager.GetSelectedObjects()[0].GetView().GetModel<ISpawner>();
                    if (selectedSpawner != null) {
                        IsSpawnerSelected = true;
                        CurrentSpawner = new SpawnerContext(selectedSpawner);
                    }
                }
                //}
            } else {
                CurrentHero = null;
                IsSpawnerSelected = false;
                CurrentSpawner = null;
                IsHeroSelected = false;
                CurrentOneUnit = null;
            }
        }

        public void UpgradeSpawnerClickButton() {
            SelectionManager.GetSelectedObjects()[0].GetView().GetModel<ISpawner>().Upgrade();
        }
    }

    public class NDataInitializer : View {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        public NguiRootContext view;

        protected override void Awake() {
            base.Awake();
            var rootContext = new GUIRootContext();
            InjectionBinder.injector.Inject(rootContext);
            view.SetContext(rootContext);
            rootContext.Initialize();
        }

    }
}
