using Assets.src.data;
using Assets.src.utils;
using strange.extensions.injector.api;
using UnityEngine;

namespace Assets.src.models {
    public class UnitFactory : IUnitFactory {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        public IUnit CreateUnit(Vector3 position, UnitTypes type, UnitData data, bool isDefender) {
            IUnit unit;
            if (type == UnitTypes.HERO) {
                unit = new HeroModel();
            } else {
                unit = new UnitModel();
            }
            InjectionBinder.injector.Inject(unit);
            unit.Spawn(position, data, type, isDefender);
            unit.InitializeStates();
            unit.StartAct();
            return unit;
        }
    }
}