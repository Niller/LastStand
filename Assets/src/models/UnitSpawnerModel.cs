using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.views;
using strange.extensions.injector.api;
using UnityEngine;

namespace Assets.src.models {
    public class UnitSpawnerModel : ISpawner {
        [Inject]
        public ICooldownService CooldownService { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

        [Inject]
        public IUnitFactory UnitFactory { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        protected Dictionary<int, UnitData> cachedUnitData = new Dictionary<int, UnitData>();

        protected ICooldownItem spawnUnitsCooldown;

        protected UnitSpawnerView view;

        protected int leftSpawn;

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
            leftSpawn = view.data.waveSpanwLimit;
            spawnUnitsCooldown = CooldownService.AddCooldown(view.data.upgradeData[view.data.level - 1].trainingSpeed,
                null, SpawnUnit);
        }

        protected void SpawnUnit() {
            if (leftSpawn > 0 || IsDefender) {
                var data = view.data;
                leftSpawn--;
                UnitFactory.CreateUnit(view.spawnPoint.position, data.produceUnitType, GetCurrentUnitData().Copy(),
                    IsDefender);
                spawnUnitsCooldown =
                    CooldownService.AddCooldown(view.data.upgradeData[view.data.level - 1].trainingSpeed,
                        null, SpawnUnit);
            }
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

        public bool IsUpgradeExist() {
            return view.data.upgradeData.Length > view.data.level;
        }

        public int GetUpgradeCost() {
            return view.data.upgradeData[view.data.level].cost;
        }

        public bool IsSpawnEnded() {
            return leftSpawn <= 0;
        }

        public void Upgrade() {
            if (GameManager.Gold.Value >= GetUpgradeCost()) {
                GameManager.Gold.Value -= GetUpgradeCost();
                view.data.level++;
            }
            
        }

        public int GetLevel() {
            return view.data.level;
        }
    }
}