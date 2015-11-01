using System;
using Assets.src.data;
using Assets.src.utils;

namespace Assets.src.models {
    public interface ISpellFactory {
        ISpell CreateSpell(Spells spellType, SpellData data, ITarget target, IAttackableTarget source);
    }
}