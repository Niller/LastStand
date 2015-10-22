namespace Assets.src.battle {
    public class GameManager : IGameManager {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        public GameManager() {
            BattleManager.Initialize();

        }
    }
}