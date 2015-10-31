using System.Collections.Generic;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.signals;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.models {
    public class HeroModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {

        [Inject]
        public OnSpellCast OnSpellCast { get; set; }

        [Inject]
        public ISpellCastManager SpellCastManager { get; set; }

        [Inject]
        public ISpellFactory SpellFactory { get; set; }

        protected List<SpellSlot> spells;

        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<HeroModel>();
        }

        protected override void Initialize() {
            base.Initialize();
            spells = new List<SpellSlot>();
            spells.Add(new SpellSlot(Spells.ICE_BOLT, 1) {data = new IceBoltData() { damage = 5 } });
            spells.Add(new SpellSlot(Spells.METEOR, 1) { data = new MeteorData() { damage = 5, range = 10} });
        }

        public List<SpellSlot> GetSpellSlots() {
            return spells;
        }

        public void TryCastSpell(ITarget target, SpellSlot slot) {
            ForceStopCurrentState();
            //OnSpellCast.Dispatch(GetView().GetPosition(), slot.spell, target, slot.data);
            SpellFactory.CreateSpell(slot.spell, slot.data, target, this);
            StartPursueOrIdle();
        }

        public override bool IsManualControl { get { return true; } }
        
    }
}