using Assets.src.data;
using Assets.src.managers;
using Assets.src.utils;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public class UnitModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {

        

        public override bool IsManualControl { get { return false; } }

        /*
        public UnitModel(Vector3 position, UnitData dataParam, UnitTypes typeParam, bool isDefenderParam) {
            
        }
        */


    }
}