using System;
using System.Security.Policy;
using Assets.src.battle;
using strange.extensions.injector.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.src.gui {

    public class UnitContext : EZData.Context {
        

        
    }

    public class GUIRootContext : EZData.Context {
        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        #region Unit
        public readonly EZData.VariableContext<UnitContext> UnitEzVariableContext = new EZData.VariableContext<UnitContext>(null);
        public UnitContext Unit {
            get { return UnitEzVariableContext.Value; }
            set { UnitEzVariableContext.Value = value; }
        }
        #endregion

        #region Property MaxHP
        private readonly EZData.Property<int> _privateMaxHPProperty = new EZData.Property<int>();
        public EZData.Property<int> MaxHPProperty { get { return _privateMaxHPProperty; } }
        public int MaxHP {
            get { return MaxHPProperty.GetValue(); }
            set { MaxHPProperty.SetValue(value); }
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

        public void Initialize() {
            SelectionManager.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged() {
            
            IsOneUnitSelected = SelectionManager.GetSelectedObjects().Count == 1;
            MaxHP = Random.Range(0, 100);
            //Debug.Log(IsOneUnitSelected);
        }
    }

    public class NDataInitializer : View {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        

        public NguiRootContext view;

        

        protected override void Start() {
            base.Start();
            var rootContext = new GUIRootContext();
            InjectionBinder.injector.Inject(rootContext);
            view.SetContext(rootContext);
            rootContext.Unit = new UnitContext();
            rootContext.MaxHP = 115;
            rootContext.Initialize();
            rootContext.IsOneUnitSelected = true;

        }

    }
}
