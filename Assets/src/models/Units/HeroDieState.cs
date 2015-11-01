using Assets.src.battle;

namespace ru.pragmatix.orbix.world.units {
    public class HeroDieState : UnitDieState {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected override void Stop() {
            BattleManager.StartRespawnHero();
            base.Stop();
        }
    }
}