using Assets.src.data;
using Assets.src.models;
using Assets.src.signals;
using Assets.src.utils;

namespace Assets.src.battle {
    public class SpellCastManager : ISpellCastManager {

        [Inject]
        public OnSpellSlotActivated OnSpellSlotActivated { get; set; }

        [Inject]
        public OnPreparationTargetSignal OnPreparationTargetSignal { get; set; }

        [Inject]
        public OnPreparationAreaSignal OnPreparationAreaSignal { get; set; }

        [Inject]
        public OnResetSpellPreparationSignal OnResetSpellPreparationSignal { get; set; }

        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        protected SpellSlot activeSpell;

        protected HeroModel selectedHero;

        [PostConstruct]
        public void Initialize() {
            SelectionManager.SelectionChanged += SelectionChanged;
            OnSpellSlotActivated.AddListener(SpellSlotActivated);
            OnResetSpellPreparationSignal.AddListener(ResetActiveSpell);
        }

        private void ResetActiveSpell() {
            activeSpell = null;
        }

        protected void SpellSlotActivated(int slotNumber) {
            if (selectedHero != null) {
                var spellSlot = selectedHero.GetSpellSlots()[slotNumber];
                if (!spellSlot.CheckCastPossibility())
                    return;
                activeSpell = spellSlot;
                if (spellSlot.data.type == SpellTypes.TARGET) {
                    OnPreparationTargetSignal.Dispatch();
                } else {
                    OnPreparationAreaSignal.Dispatch((spellSlot.data as MeteorData).range);
                }
            }
        }

        private void SelectionChanged() {
            foreach (var selectable in SelectionManager.GetSelectedObjects()) {
                var hero = selectable.GetView().GetModel<HeroModel>();
                if (hero != null) {
                    selectedHero = hero;
                    return;
                }
            }
            selectedHero = null;
            OnUnselectHero();
        }

        protected void OnUnselectHero() {
            ResetActiveSpell();
        }

        public bool IsReadyToCastSpell() {
            return selectedHero != null && activeSpell != null;
        }

        public SpellSlot GetActiveSpell() {
            return activeSpell;
        }

        public void CastCurrentActiveSpell(ITarget target) {
            selectedHero.TryCastSpell(target, activeSpell);
        }

    }
}