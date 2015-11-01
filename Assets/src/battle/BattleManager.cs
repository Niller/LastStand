using System;
using System.Collections.Generic;
using Assets.Common.Extensions;
using Assets.src.data;
using Assets.src.models;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.utils;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.src.battle {
    public class BattleManager : IBattleManager {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        private readonly List<ITarget> attackers;

        private readonly List<ITarget> defenders;

        private readonly List<ISpawner> attackersSpawners;

        private readonly List<ISpawner> defendersSpawners;

        private FontainModel fontain;

        private HeroData currentHeroData;

        public Action OnHeroRespawnStart { get; set; }

        public Action OnHeroRespawnEnd { get; set; }

        protected int enemiesUnitsLeftSpawn;

        public BattleManager() {
            attackers = new List<ITarget>();
            defenders = new List<ITarget>();
            attackersSpawners = new List<ISpawner>();
            defendersSpawners = new List<ISpawner>();
        }

        public void StartRound() {
            
            enemiesUnitsLeftSpawn = GameDataService.GetConfig().roundEnemiesCount;
            if (attackers.Count != 0) {
                Debug.LogError(attackers.Count);
            }
            
            foreach (var attackersSpawner in attackersSpawners) {
                attackersSpawner.StartSpawn();
            }
            foreach (var defendersSpawner in defendersSpawners) {
                defendersSpawner.StartSpawn();
            }
        }

        public void StopRound() {
            
            foreach (var attackersSpawner in attackersSpawners) {
                attackersSpawner.StopSpawn();
            }
            foreach (var defendersSpawner in defendersSpawners) {
                defendersSpawner.StopSpawn();
            }
        }

        private List<ITarget> GetAppropriateListForTarget(ITarget target) {
            return target.GetTargetBehaviour().IsDefender ? defenders : attackers;
        }

        private List<ISpawner> GetAppropriateListForSpawner(ISpawner spawner) {
            return spawner.IsDefender ? defendersSpawners : attackersSpawners;
        }

        public void UpgradeEnemiesSpawners() {
            attackersSpawners.ForEach(spawner => spawner.Upgrade());
        }

        public void Initialize() {
            
            //CooldownService.AddCooldown(3f, null, StartRound);

        }

        public void RegisterTarget(ITarget target) {
            if (!target.GetTargetBehaviour().IsDefender)
                enemiesUnitsLeftSpawn--;
            if (enemiesUnitsLeftSpawn <= 0) {
                attackersSpawners.ForEach(spawner => spawner.StopSpawn());
            }
            GetAppropriateListForTarget(target).Add(target);
        }

        public void UnregisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Remove(target);
            //Debug.Log(attackers.Count + " " + enemiesUnitsLeftSpawn);
            if (attackers.Count == 0 && enemiesUnitsLeftSpawn <= 0) {
                StopRound();
                GameManager.NextRound();
            }
        }

        public void RegisterSpawner(ISpawner spawner) {
            GetAppropriateListForSpawner(spawner).Add(spawner);
        }

        public List<ITarget> GetDefenders() {
            return defenders;
        }

        public List<ITarget> GetAttackers() {
            return attackers;
        }

        public void RegisterFontaion(FontainModel fontainParam) {
            fontain = fontainParam;
            fontain.SpawnHero();
        }

        public HeroData GetCurrentHeroData() {
            return currentHeroData;

        }

        public void SaveCurrentHeroData(HeroData data) {
            currentHeroData = data;
        }

        public void StartRespawnHero() {
            fontain.StartHeroRespawn();
            OnHeroRespawnStart.TryCall();
        }

        public ICooldownItem GetHeroRespawnCooldown() {
            return fontain.GetHeroRespawnCooldown();
        }

        public void OnHeroRespawned() {
            OnHeroRespawnEnd.TryCall();
        }
        
    }
}