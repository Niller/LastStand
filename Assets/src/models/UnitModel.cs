using Assets.src.data;
using Assets.src.managers;
using Assets.src.utils;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public class UnitModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {
        public override bool IsManualControl { get { return false; } }


        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<UnitModel>((int)type); ;
        }

    }
}