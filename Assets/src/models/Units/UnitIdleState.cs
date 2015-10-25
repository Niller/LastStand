using Assets.src.services;

namespace ru.pragmatix.orbix.world.units {
    public class UnitIdleState : BaseUnitState, IUnitIdleState {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        protected ICooldownItem idleCooldown;

        public override void Start() {
            idleCooldown = CooldownService.AddCooldown(0.5f, null, Stop, 0, 0.1f);
        }

        public override void Update() {
            
        }

        public override void ForceStop() {
            
        }
    }
}