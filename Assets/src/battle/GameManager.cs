using System;
using System.Net.Mime;
using Assets.src.models;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.utils;
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

        public Action<ICooldownItem, int> OnNextRoundStartCountdown { get; set; }

        public void Initialize() {
            Gold = new ObservableProperty<int>(150);
            BattleManager.Initialize();
            CooldownService.AddCooldown(1f, null, NextRound);
        }

        public void NextRound() {
            if (currentRound < GameDataService.GetConfig().roundCount) {
                if (currentRound != 0) {
                    BattleManager.UpgradeEnemiesSpawners();
                }
                currentRound++;
                var cd = CooldownService.AddCooldown(GameDataService.GetConfig().timeBeforeRoundStart, null,
                    BattleManager.StartRound);
                OnNextRoundStartCountdown.TryCall(cd, currentRound);
            } else {
                Application.Quit();
            }
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