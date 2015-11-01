using Assets.src.models;
using strange.extensions.injector.api;

namespace Assets.src.gui {
    public class HeroContext : EZData.Context {

        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        #region Slot1
        public readonly EZData.VariableContext<SpellSlotUi> Slot1EzVariableContext = new EZData.VariableContext<SpellSlotUi>(null);
        public SpellSlotUi Slot1 {
            get { return Slot1EzVariableContext.Value; }
            set { Slot1EzVariableContext.Value = value; }
        }
        #endregion

        #region Slot2
        public readonly EZData.VariableContext<SpellSlotUi> Slot2EzVariableContext = new EZData.VariableContext<SpellSlotUi>(null);
        public SpellSlotUi Slot2 {
            get { return Slot2EzVariableContext.Value; }
            set { Slot2EzVariableContext.Value = value; }
        }
        #endregion

        #region Property UpgradePoints
        private readonly EZData.Property<int> _privateUpgradePointsProperty = new EZData.Property<int>();
        public EZData.Property<int> UpgradePointsProperty { get { return _privateUpgradePointsProperty; } }
        public int UpgradePoints {
            get { return UpgradePointsProperty.GetValue(); }
            set { UpgradePointsProperty.SetValue(value); }
        }
        #endregion

        #region Property IsUpgradeMode
        private readonly EZData.Property<bool> _privateIsUpgradeModeProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsUpgradeModeProperty { get { return _privateIsUpgradeModeProperty; } }
        public bool IsUpgradeMode {
            get { return IsUpgradeModeProperty.GetValue(); }
            set { IsUpgradeModeProperty.SetValue(value); }
        }
        #endregion

        protected HeroModel hero;

        public void InitializeSlots(HeroModel heroParam) {
            hero = heroParam;
            var slots = hero.GetSpellSlots();
            Slot1 = new SpellSlotUi(this, hero, slots[0], GameDataService.GetIconBySpell(slots[0].spell));
            Slot2 = new SpellSlotUi(this, hero, slots[1], GameDataService.GetIconBySpell(slots[1].spell));
            UpgradePoints = hero.UpgradePoints.Value;
            hero.UpgradePoints.OnPropertyChanged += OnUpgradePointsChanged;
        }

        private void OnUpgradePointsChanged(int points) {
            UpgradePoints = points;
            if (IsUpgradeMode) {
                IsUpgradeMode = UpgradePoints > 0;
            }
        }

        public void UpgradeClickButton() {
            IsUpgradeMode = !IsUpgradeMode;
        }
    }
}