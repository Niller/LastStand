namespace Assets.src.battle {
    public interface IGameManager {
        void BlockControl();
        bool IsControlBlocked();
        void UnblockControl();
    }
}