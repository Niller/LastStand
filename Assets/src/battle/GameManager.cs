using Assets.src.models;
using Assets.src.signals;

namespace Assets.src.battle {
    public class GameManager : IGameManager {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected bool isControlBlocked;

        public ObservableProperty<int> Gold { get; set; } 

        public void Initialize() {
            Gold = new ObservableProperty<int>(150);
            BattleManager.Initialize();
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