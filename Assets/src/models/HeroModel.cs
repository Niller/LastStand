using System;
using System.Collections.Generic;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.signals;
using Assets.src.utils;
using ru.pragmatix.orbix.world.units;
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

        [Inject]
        public IBattleManager BattleManager { get; set; }

        public ObservableProperty<int> UpgradePoints { get; set; }

        public ObservableProperty<int> ExperiencePoints { get; set; }

        public ObservableProperty<int> Level { get; set; }

        protected List<SpellSlot> spells;

        protected HeroData heroData;

        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<HeroModel>();
        }

        protected override void Initialize() {
            base.Initialize();
            heroData = data as HeroData;
            UpgradePoints = new ObservableProperty<int>(heroData.upgradePoints);
            ExperiencePoints = new ObservableProperty<int>(heroData.xp);
            Level = new ObservableProperty<int>(heroData.level);
            spells = new List<SpellSlot>();
            var slot1 = new SpellSlot() {data = GameDataService.GetConfig().iceBoltLevelsData[heroData.spellLevels[0]] };
            slot1.Initialize(Spells.ICE_BOLT, heroData.spellLevels[0]);
            InjectionBinder.injector.Inject(slot1);
            spells.Add(slot1);
            var slot2 = new SpellSlot() {data = GameDataService.GetConfig().meteorLevelsData[heroData.spellLevels[1]] };
            slot2.Initialize(Spells.METEOR, heroData.spellLevels[1]);
            InjectionBinder.injector.Inject(slot2);
            spells.Add(slot2);
        }

        protected override void InitDieState() {
            dieState = new HeroDieState();
            InjectionBinder.injector.Inject(dieState);
            dieState.Initialize(this, null);
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

        protected override void EnterDieState() {
            base.EnterDieState();
            var newData = heroData.Copy();
            newData.upgradePoints = UpgradePoints.Value;
            newData.xp = ExperiencePoints.Value;
            newData.level = Level.Value;
            newData.spellLevels = new[] {spells[0].level, spells[1].level};
            BattleManager.SaveCurrentHeroData(newData);
        }
        
    }
}