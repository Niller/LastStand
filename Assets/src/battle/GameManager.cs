using System.Net.Mime;
using Assets.src.models;
using Assets.src.services;
using Assets.src.signals;
using UnityEngine;

namespace Assets.src.battle {
    public class GameManager : IGameManager {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public ICooldownService CooldownService { get; set; }

        protected bool isControlBlocked;

        public ObservableProperty<int> Gold { get; set; }

        protected int currentRound = 0;

        public void Initialize() {
            Gold = new ObservableProperty<int>(150);
            BattleManager.Initialize();
            NextRound();
        }

        public void NextRound() {
            Debug.Log("NextRound");
            if (currentRound < GameDataService.GetConfig().roundCount) {
                if (currentRound != 0) {
                    BattleManager.UpgradeEnemiesSpawners();
                }
                currentRound++;
                CooldownService.AddCooldown(GameDataService.GetConfig().timeBeforeRoundStart, Test,
                    BattleManager.StartRound);
            } else {
                Application.Quit();
            }
        }

        private void Test() {
            Debug.Log(Time.time);
        }

        public void BlockControl() {
            isControlBlocked = true;
        }

        public bool IsControlBlocked() {
            return isControlBlocked;
        }

        public void UnblockControl() {
            isControlBlocked = false;
        }
    }
}