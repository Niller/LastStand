using Assets.src.data;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.models {
    public interface IUnitFactory {
        IUnit CreateUnit(Vector3 position, UnitTypes type, UnitData data, bool isDefender);
    }
}