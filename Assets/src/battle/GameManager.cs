using Assets.src.signals;

namespace Assets.src.battle {
    public class GameManager : IGameManager {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected bool isControlBlocked;


        /*[PostConstruct]
        public void GameManagerInitialize() {
            BattleManager.Initialize();

        }
        */

        public GameManager() {
            //BattleManager.Initialize();

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