using System;
using Assets.src.models;
using Assets.src.services;

namespace Assets.src.battle {
    public interface IGameManager {
        void BlockControl();
        bool IsControlBlocked();
        void UnblockControl();
        ObservableProperty<int> Gold { get; set; }
        Action<ICooldownItem, int> OnNextRoundStartCountdown { get; set; }
        void Initialize();
        void NextRound();
    }
}