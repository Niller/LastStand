using System.Collections.Generic;
using Assets.src.data;
using Assets.src.models;
using Assets.src.signals;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.battle {
    public class BattleManager : IBattleManager {

        [Inject]
        public OnCreateUnitSignal CreateUnitSignal { get; set; }

        private readonly List<ITarget> attackers;

        private readonly List<ITarget> defenders;

        private readonly List<ISpawner> attackersSpawners;

        private readonly List<ISpawner> defendersSpawners;

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
            return target.IsDefender ? defenders : attackers;
        }

        private List<ISpawner> GetAppropriateListForSpawner(ISpawner spawner) {
            return spawner.IsDefender ? defendersSpawners : attackersSpawners;
        }

        public void Initialize() {
            //CreateUnitSignal.Dispatch(new Vector3(50,0,50), UnitTypes.ENEMY_MELEE, new UnitData(), false);
            StartRound();
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
    }
}