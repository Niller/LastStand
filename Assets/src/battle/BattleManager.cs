using System.Collections.Generic;
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
        public OnCreateUnitSignal CreateUnitSignal { get; set; }

        [Inject]
        public ICooldownService CooldownService { get; set; }

        private readonly List<ITarget> attackers;

        private readonly List<ITarget> defenders;

        private readonly List<ISpawner> attackersSpawners;

        private readonly List<ISpawner> defendersSpawners;

        private FontainModel fontain;

        public BattleManager() {
            attackers = new List<ITarget>();
            defenders = new List<ITarget>();
            attackersSpawners = new List<ISpawner>();
            defendersSpawners = new List<ISpawner>();
        }

        public void StartRound() {
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

        public void Initialize() {
            CooldownService.AddCooldown(3f, null, StartRound);
            //fontain.SpawnHero();
        }

        public void RegisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Add(target);
        }

        public void UnregisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Remove(target);
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
        }
    }
}