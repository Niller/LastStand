using Assets.src.models;

namespace Assets.src.gui {
    public class SpawnerContext : EZData.Context {

        protected ISpawner spawner;

        public SpawnerContext(ISpawner spawnerParam) {
            spawner = spawnerParam;
            Reset();
        }

        public void UpgradeSpawnerClickButton() {
            spawner.Upgrade();
            Reset();
        }

        protected void Reset() {
            SpawnerLevel = spawner.GetLevel();
            UpgradeExist = spawner.IsUpgradeExist();
            if (UpgradeExist) {
                UpgradeCost = spawner.GetUpgradeCost();
                SpawnerLevel = spawner.GetLevel();
            }
        }

        #region Property UpgradeExist
        private readonly EZData.Property<bool> _privateUpgradeExistProperty = new EZData.Property<bool>();
        public EZData.Property<bool> UpgradeExistProperty { get { return _privateUpgradeExistProperty; } }
        public bool UpgradeExist {
            get { return UpgradeExistProperty.GetValue(); }
            set { UpgradeExistProperty.SetValue(value); }
        }
        #endregion

        #region Property UpgradeCost
        private readonly EZData.Property<int> _privateUpgradeCostProperty = new EZData.Property<int>();
        public EZData.Property<int> UpgradeCostProperty { get { return _privateUpgradeCostProperty; } }
        public int UpgradeCost {
            get { return UpgradeCostProperty.GetValue(); }
            set { UpgradeCostProperty.SetValue(value); }
        }
        #endregion

        #region Property SpawnerLevel
        private readonly EZData.Property<int> _privateSpawnerLevelProperty = new EZData.Property<int>();
        public EZData.Property<int> SpawnerLevelProperty { get { return _privateSpawnerLevelProperty; } }
        public int SpawnerLevel {
            get { return SpawnerLevelProperty.GetValue(); }
            set { SpawnerLevelProperty.SetValue(value); }
        }
        #endregion
    }
}