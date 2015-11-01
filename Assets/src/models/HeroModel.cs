using System;
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

        [Inject]
        public IGameManager GameManager { get; set; }

        public ObservableProperty<int> UpgradePoints { get; set; }

        public ObservableProperty<int> ExperiencePoints { get; set; }

        public ObservableProperty<int> Level { get; set; }

        protected List<SpellSlot> spells;

        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<HeroModel>();
        }

        protected override void Initialize() {
            base.Initialize();
            UpgradePoints = new ObservableProperty<int>(0);
            ExperiencePoints = new ObservableProperty<int>(1);
            Level = new ObservableProperty<int>(1);
            spells = new List<SpellSlot>();
            var slot1 = new SpellSlot() {data = GameDataService.GetConfig().iceBoltLevelsData[0]};
            slot1.Initialize(Spells.ICE_BOLT, 1);
            InjectionBinder.injector.Inject(slot1);
            spells.Add(slot1);
            var slot2 = new SpellSlot() {data = GameDataService.GetConfig().meteorLevelsData[0]};
            slot2.Initialize(Spells.METEOR, 1);
            InjectionBinder.injector.Inject(slot2);
            spells.Add(slot2);
        }

        public List<SpellSlot> GetSpellSlots() {
            return spells;
        }

        public void TryCastSpell(ITarget target, SpellSlot slot) {
            ForceStopCurrentState();
            //OnSpellCast.Dispatch(GetView().GetPosition(), slot.spell, target, slot.data);
            SpellFactory.CreateSpell(slot.spell, slot.data, target, this);
            slot.StartCooldown();
            StartPursueOrIdle();
        }

        /*public void UpgradeSpell(int slotNumber) {
            spells[slotNumber].Upgrade();
        }
        */

        public void UpgradeSpell(SpellSlot slot) {
            slot.Upgrade();
            UpgradePoints.Value--;
        }

        public override void DoDamage(ITarget target, int damage) {
            if (target.GetTargetBehaviour().SetDamage(damage)) {
                var targetWithReward = target as ITargetWithReward;
                if (targetWithReward != null) {
                    AddExperiencePoints(targetWithReward.GetXPReward());
                    GameManager.Gold.Value += targetWithReward.GetGoldReward();
                }
            }
        }

        protected void AddExperiencePoints(int count) {
            ExperiencePoints.Value += count;
            if (ExperiencePoints.Value >= GameDataService.GetConfig().heroXpStep) {
                LevelUp();    
            }
        }

        protected void LevelUp() {
            ExperiencePoints.Value -= GameDataService.GetConfig().heroXpStep;
            Level.Value++;
            UpgradePoints.Value++;
        }

        public override bool IsManualControl { get { return true; } }
        
    }
}