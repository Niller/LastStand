namespace Assets.src.models {
    public interface ISpawner {
        bool IsDefender { get; }
        void StartSpawn();
        void StopSpawn();
    }
}