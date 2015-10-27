using Assets.src.data;
using Assets.src.signals;

namespace Assets.src.models {
    public class HeroModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {

        [Inject]
        public OnSpellSlotActivated OnSpellSlotActivated { get; set; }

        [Inject]
        public OnPreparationTargetSignal OnPreparationTargetSignal { get; set; }

        [Inject]
        public OnPreparationAreaSignal OnPreparationAreaSignal { get; set; }

        public override void Initialize(BaseBattleInformer informerParam) {
            base.Initialize(informerParam); 
            OnSpellSlotActivated.AddListener(ActivateSpellSlot);
        }

        private void ActivateSpellSlot(int slot) {
            if (slot == 0) {
                OnPreparationTargetSignal.Dispatch();
            }
            else {
                OnPreparationAreaSignal.Dispatch(10);
            }
        }

        protected override void Destroy() {
            base.Destroy();
            OnSpellSlotActivated.RemoveListener(ActivateSpellSlot);
        }

        public override bool IsManualControl { get { return true; } }
    }
}