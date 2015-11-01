using Assets.src.models;

namespace Assets.src.battle {
    public interface ISpellCastManager {
        bool IsReadyToCastSpell();
        SpellSlot GetActiveSpell();
        void CastCurrentActiveSpell(ITarget target);
    }
}