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

    public class SpellSlotUI : EZData.Context {

        public SpellSlotUI(int level, string icon) {
            Level = level;
            Icon = icon;
        }

        #region Property Level
        private readonly EZData.Property<int> _privateLevelProperty = new EZData.Property<int>();
        public EZData.Property<int> LevelProperty { get { return _privateLevelProperty; } }
        public int Level {
            get { return LevelProperty.GetValue(); }
            set { LevelProperty.SetValue(value); }
        }
        #endregion

        #region Property Icon
        private readonly EZData.Property<string> _privateIconProperty = new EZData.Property<string>();
        public EZData.Property<string> IconProperty { get { return _privateIconProperty; } }
        public string Icon {
            get { return IconProperty.GetValue(); }
            set { IconProperty.SetValue(value); }
        }
        #endregion
    }

    public class HeroContext : EZData.Context {

        [Inject]
        public IGameDataService GameDataService { get; set; }


        #region Slot1
        public readonly EZData.VariableContext<SpellSlotUI> Slot1EzVariableContext = new EZData.VariableContext<SpellSlotUI>(null);
        public SpellSlotUI Slot1 {
            get { return Slot1EzVariableContext.Value; }
            set { Slot1EzVariableContext.Value = value; }
        }
        #endregion

        #region Slot2
        public readonly EZData.VariableContext<SpellSlotUI> Slot2EzVariableContext = new EZData.VariableContext<SpellSlotUI>(null);
        public SpellSlotUI Slot2 {
            get { return Slot2EzVariableContext.Value; }
            set { Slot2EzVariableContext.Value = value; }
        }
        #endregion

        public void InitializeSlots(HeroModel hero) {
            var slots = hero.GetSpellSlots();
            Slot1 = new SpellSlotUI(slots[0].level, GameDataService.GetIconBySpell(slots[0].spell));
            Slot2 = new SpellSlotUI(slots[1].level, GameDataService.GetIconBySpell(slots[1].spell));
        }

    }


    public class UnitContext : EZData.Context {

        protected ITargetBehaviour targetBehaviour;

        public void SetTargetBehaviour(ITargetBehaviour targetBehaviourParam) {
            targetBehaviour = targetBehaviourParam;
            MaxHP = targetBehaviour.GetMaxHP();
            CurrentHP = targetBehaviour.GetCurrentHP();
            targetBehaviour.OnHPChanged += OnHPChanged;
        }

        private void OnHPChanged() {
            CurrentHP = targetBehaviour.GetCurrentHP();
        }

        #region Property MaxHP
        private readonly EZData.Property<int> _privateMaxHPProperty = new EZData.Property<int>();
        public EZData.Property<int> MaxHPProperty { get { return _privateMaxHPProperty; } }
        public int MaxHP {
            get { return MaxHPProperty.GetValue(); }
            set { MaxHPProperty.SetValue(value); }
        }
        #endregion

        #region Property CurrentHP
        private readonly EZData.Property<int> _privateCurrentHPProperty = new EZData.Property<int>();
        public EZData.Property<int> CurrentHPProperty { get { return _privateCurrentHPProperty; } }
        public int CurrentHP {
            get { return CurrentHPProperty.GetValue(); }
            set {
                HPPercent = value/(float)MaxHP;
                CurrentHPProperty.SetValue(value);
            }
        }
        #endregion

        #region Property HPPercent
        private readonly EZData.Property<float> _privateHPPercentProperty = new EZData.Property<float>();
        public EZData.Property<float> HPPercentProperty { get { return _privateHPPercentProperty; } }
        public float HPPercent {
            get { return HPPercentProperty.GetValue(); }
            set { HPPercentProperty.SetValue(value); }
        }
        #endregion

    }

    public class GUIRootContext : EZData.Context {
        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

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

        public void Initialize() {
            SelectionManager.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged() {
            IsOneUnitSelected = SelectionManager.GetSelectedObjects().Count == 1;
            if (IsOneUnitSelected) {
                var selectedObject =
                    SelectionManager.GetSelectedObjects()[0].GetView().GetModel<ITarget>();
                if (selectedObject is HeroModel) {
                    IsHeroSelected = true;
                    CurrentHero = new HeroContext();
                    //CurrentHero.SetTargetBehaviour(selectedObject.GetTargetBehaviour());
                    InjectionBinder.injector.Inject(CurrentHero);
                    CurrentHero.InitializeSlots(selectedObject as HeroModel);
                } else {
                    IsHeroSelected = false;
                    CurrentHero = null;
                }//else {
                  
                    CurrentOneUnit = new UnitContext();
                    CurrentOneUnit.SetTargetBehaviour(selectedObject.GetTargetBehaviour());
                //}
            } else {
                CurrentHero = null;
                IsHeroSelected = false;
                CurrentOneUnit = null;
            }
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
