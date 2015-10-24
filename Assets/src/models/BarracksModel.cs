using System.CodeDom;
using System.Collections.Generic;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.mediators;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public class BarracksModel : IModel, ISpawner {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

        [Inject]
        public OnCreateUnitSignal CreateUnitSignal { get; set; }

        public BarracksMediator Mediator { protected get; set; }

        protected Dictionary<int, UnitData> cachedUnitData = new Dictionary<int, UnitData>();

        protected ICooldownItem spawnUnitsCooldown;

        public void SetView(IView view) {
            
        }

        public bool IsDefender { get { return Mediator.Informer.isDefender; } }

        public void Initialize() {
            BattleManager.RegisterSpawner(this);
        }

        public void StartSpawn() {
            spawnUnitsCooldown = CooldownService.AddCooldown(Mediator.Informer.data.upgradeData[Mediator.Informer.data.level].trainingSpeed,
                null, SpawnUnit);
        }

        protected void SpawnUnit() {
            var data = Mediator.Informer.data;
            CreateUnitSignal.Dispatch(Mediator.Informer.spawnPoint.position, data.produceUnitType, GetCurrentUnitData(), IsDefender);
            spawnUnitsCooldown = CooldownService.AddCooldown(Mediator.Informer.data.upgradeData[Mediator.Informer.data.level].trainingSpeed,
                null, SpawnUnit);
        }

        private UnitData GetCurrentUnitData() {
            var data = Mediator.Informer.data;
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