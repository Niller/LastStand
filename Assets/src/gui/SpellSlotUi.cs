using Assets.src.models;

namespace Assets.src.gui {
    public class SpellSlotUi : EZData.Context {

        protected SpellSlot slot;

        protected HeroModel heroModel;

        public SpellSlotUi(HeroContext heroParam, HeroModel heroModelParam, SpellSlot slotParam, string icon) {
            slot = slotParam;
            Level = slot.level;
            Icon = icon;
            Parent = heroParam;
            heroModel = heroModelParam;

            slot.OnStartCooldown += OnStartCooldown;
            slot.OnEndCooldown += OnEndCooldown;
            if (slot.Cooldown != null) {
                IsOnCooldown = true;
                slot.Cooldown.OnTick += OnTick;
            } else {
                IsOnCooldown = false;
            }
        }

        private void OnEndCooldown() {
            IsOnCooldown = false;
        }

        private void OnStartCooldown() {
            slot.Cooldown.OnTick += OnTick;
            IsOnCooldown = true;
        }

        private void OnTick() {
            CooldownPercent = 1f - slot.Cooldown.GetPCT();
        }

        #region Parent
        public readonly EZData.VariableContext<HeroContext> ParentEzVariableContext = new EZData.VariableContext<HeroContext>(null);
        public HeroContext Parent {
            get { return ParentEzVariableContext.Value; }
            set { ParentEzVariableContext.Value = value; }
        }
        #endregion

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

        #region Property CooldownPercent
        private readonly EZData.Property<float> _privateCooldownPercentProperty = new EZData.Property<float>();
        public EZData.Property<float> CooldownPercentProperty { get { return _privateCooldownPercentProperty; } }
        public float CooldownPercent {
            get { return CooldownPercentProperty.GetValue(); }
            set { CooldownPercentProperty.SetValue(value); }
        }
        #endregion

        #region Property IsOnCooldown
        private readonly EZData.Property<bool> _privateIsOnCooldownProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsOnCooldownProperty { get { return _privateIsOnCooldownProperty; } }
        public bool IsOnCooldown {
            get { return IsOnCooldownProperty.GetValue(); }
            set { IsOnCooldownProperty.SetValue(value); }
        }
        #endregion

        public void OnSlotClick() {
            if (!slot.CheckUpgradePossibility())
                return;
            if (Parent.IsUpgradeMode)
                heroModel.UpgradeSpell(slot);
        }
    }
}