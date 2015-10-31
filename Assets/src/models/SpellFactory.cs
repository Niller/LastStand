using System;
using Assets.src.data;
using Assets.src.utils;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace Assets.src.models {
    public class SpellFactory : ISpellFactory {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        public ISpell CreateSpell(Spells spellType, SpellData data, ITarget target, ITarget source) {
            ISpell spellModel = null;
            switch (spellType) {
                case Spells.ICE_BOLT:
                    spellModel = Activator.CreateInstance<IceBoltModel>();
                    break;
                case Spells.METEOR:
                    spellModel = Activator.CreateInstance<MeteorModel>();
                    break;
            }
            InjectionBinder.injector.Inject(spellModel);
            spellModel.Initialize(source, target, data);
            return spellModel;
        }
    }
}