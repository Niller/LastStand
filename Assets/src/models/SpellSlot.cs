using Assets.src.data;  
using Assets.src.utils;

namespace Assets.src.models {
    public class SpellSlot {

        [Inject]
        public IGameDataService GameDataService { get; set; }

        public void Initialize(Spells spellParam, int levelParam) {
            spell = spellParam;
            level = levelParam;
        }

        public SpellData data;
        public Spells spell;
        public int level;

        public void Upgrade() {
            level++;
            if (spell == Spells.ICE_BOLT) {
                data = GameDataService.GetConfig().iceBoltLevelsData[level - 1];
            } else {
                data = GameDataService.GetConfig().meteorLevelsData[level - 1];
            }
        }
    }
}