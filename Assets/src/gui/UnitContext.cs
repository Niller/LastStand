using Assets.src.models;

namespace Assets.src.gui {
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
}