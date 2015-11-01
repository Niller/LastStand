namespace Assets.src.models {
    public interface ISpawner : IModel {
        bool IsDefender { get; }
        void StartSpawn();
        void StopSpawn();
        bool IsUpgradeExist();
        int GetUpgradeCost();
        void Upgrade();
        int GetLevel();
    }
}