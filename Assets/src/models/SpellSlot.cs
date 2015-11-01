using System;
using Assets.Common.Extensions;
using Assets.src.data;
using Assets.src.services;
using Assets.src.utils;

namespace Assets.src.models {
    public class SpellSlot {

        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public ICooldownService CooldownService { get; set; }

        public Action OnStartCooldown { get; set; }

        public Action OnEndCooldown { get; set; }

        public ICooldownItem Cooldown { get; set; }

        public void Initialize(Spells spellParam, int levelParam) {
            spell = spellParam;
            level = levelParam;
        }

        public SpellData data;
        public Spells spell;
        public int level;

        public bool CheckCastPossibility() {
            return Cooldown == null;
        }

        public void StartCooldown() {
            Cooldown = CooldownService.AddCooldown(data.cooldown, null, OnCooldownEnd, 0, 0.1f);
            OnStartCooldown.TryCall();
        }

        private void OnCooldownEnd() {
            Cooldown = null;
            OnEndCooldown.TryCall();
        }

        public void Upgrade() {
            level++;
            if (spell == Spells.ICE_BOLT) {
                data = GameDataService.GetConfig().iceBoltLevelsData[level - 1];
            } else {
                data = GameDataService.GetConfig().meteorLevelsData[level - 1];
            }
        }

        public bool CheckUpgradePossibility() {
            if (spell == Spells.ICE_BOLT) {
                return GameDataService.GetConfig().iceBoltLevelsData.Length > level;
            } else {
                return GameDataService.GetConfig().meteorLevelsData.Length > level;
            }
        }
    }
}