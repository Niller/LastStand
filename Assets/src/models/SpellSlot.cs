using Assets.src.data;
using Assets.src.utils;

namespace Assets.src.models {
    public class SpellSlot {
        public SpellSlot(Spells spellParam, int levelParam) {
            spell = spellParam;
            level = levelParam;
        }

        public SpellData data;
        public Spells spell;
        public int level;
        public bool isPrepared;

        public void Upgrade() {
            level++;
        }
    }
}