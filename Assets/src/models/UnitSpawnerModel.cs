using System.Collections.Generic;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.views;
using strange.extensions.injector.api;

namespace Assets.src.models {
    public class UnitSpawnerModel : IModel, ISpawner {
        [Inject]
        public ICooldownService CooldownService { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

        [Inject]
        public IUnitFactory UnitFactory { get; set; }

        protected Dictionary<int, UnitData> cachedUnitData = new Dictionary<int, UnitData>();

        protected ICooldownItem spawnUnitsCooldown;

        protected UnitSpawnerView view;

        [PostConstruct]
        public void UnitSpawnerModelConstructor() {
            Initialize();
        }

        public void SetView(IView viewParam) {
            view = viewParam as UnitSpawnerView;;
        }

        public IView GetView() {
            return view;
        }

        public bool IsDefender { get { return view.isDefender; } }

        public void Initialize() {
            BattleManager.RegisterSpawner(this);
        }

        public void StartSpawn() {
            spawnUnitsCooldown = CooldownService.AddCooldown(view.data.upgradeData[view.data.level].trainingSpeed,
                null, SpawnUnit);
        }

        protected void SpawnUnit() {
            var data = view.data;
            UnitFactory.CreateUnit(view.spawnPoint.position, data.produceUnitType, GetCurrentUnitData().Copy(), IsDefender);          
            spawnUnitsCooldown = CooldownService.AddCooldown(view.data.upgradeData[view.data.level].trainingSpeed,
                null, SpawnUnit);
        }

        private UnitData GetCurrentUnitData() {
            var data = view.data;
            if (cachedUnitData.ContainsKey(data.level)) {
                return cachedUnitData[data.level];
            }
            UnitData unitData = new UnitData();
            for (int i = 0; i < data.level; i++) {
                unitData += data.upgradeData[i].unitDeltaStats;
            }
            return unitData;
        }

        public void StopSpawn() {
            CooldownService.RemoveCooldown(spawnUnitsCooldown);
        }
    }
}