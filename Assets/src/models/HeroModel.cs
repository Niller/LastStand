using System.Collections.Generic;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.signals;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.models {
    public class HeroModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {

        [Inject]
        public OnSpellSlotActivated OnSpellSlotActivated { get; set; }

        [Inject]
        public OnPreparationTargetSignal OnPreparationTargetSignal { get; set; }

        [Inject]
        public OnPreparationAreaSignal OnPreparationAreaSignal { get; set; }

        [Inject]
        public OnResetSpellPreparationSignal OnResetSpellPreparationSignal { get; set; }

        [Inject]
        public OnSpellCast OnSpellCast { get; set; }

        protected List<SpellSlot> spells;

        protected SpellSlot preparedSpell;

        public override void Initialize(BaseBattleInformer informerParam) {
            base.Initialize(informerParam);
            spells = new List<SpellSlot>();
            spells.Add(new SpellSlot(Spells.ICE_BOLT, 1) {data = new IceBoltData() { damage = 5 } });
            spells.Add(new SpellSlot(Spells.METEOR, 1) { data = new MeteorData() { damage = 5, range = 10} });
            OnResetSpellPreparationSignal.AddListener(ResetActivationSpellSlot);
            OnSpellSlotActivated.AddListener(ActivateSpellSlot);
        }

        private void ActivateSpellSlot(int slot) {
            if (GameDataService.GetSpellType(spells[slot].spell) == SpellTypes.TARGET) {
                OnPreparationTargetSignal.Dispatch();
            } else {
                OnPreparationAreaSignal.Dispatch((spells[slot].data as MeteorData).range);
            }
            preparedSpell = spells[slot];
        }

        private void ResetActivationSpellSlot() {
            preparedSpell = null;
        }

        public SpellTypes? GetActiveSpellType() {
            if (preparedSpell == null)
                return null;
            return GameDataService.GetSpellType(preparedSpell.spell);
        }

        public void TryCastSpell(ITarget target) {
            if (preparedSpell != null) {
                ForceStopCurrentState();
                
                OnSpellCast.Dispatch(GetPosition(), preparedSpell.spell, target, preparedSpell.data);
                preparedSpell = null;
                StartPursueOrIdle();
            }
        }

        protected override void Destroy() {
            base.Destroy();
            OnSpellSlotActivated.RemoveListener(ActivateSpellSlot);
            OnResetSpellPreparationSignal.RemoveListener(ResetActivationSpellSlot);
        }

        public override bool IsManualControl { get { return true; } }
    }
}