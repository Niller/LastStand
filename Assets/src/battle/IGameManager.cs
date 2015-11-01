using Assets.src.models;

namespace Assets.src.battle {
    public interface IGameManager {
        void BlockControl();
        bool IsControlBlocked();
        void UnblockControl();
        ObservableProperty<int> Gold { get; set; }
        void Initialize();
        void NextRound();
    }
}