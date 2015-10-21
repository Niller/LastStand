namespace Assets.src.battle {
    public interface IBattleManager {
        void Initialize();
        void RegisterTarget(ITarget target);
        void UnregisterTarget(ITarget target);
    }
}