using Assets.src.battle;
using UnityEngine;

namespace Assets.src.models {
    public class DistanceTargetSelector : ITargetSelector {

        protected ITargetProvider provider;

        protected ITarget currentUnit;

        public void SetProvider(ITargetProvider providerParam) {
            provider = providerParam;
        }

        public void SetCurrentUnit(ITarget currentUnitParam) {
            currentUnit = currentUnitParam;
        }

        public ITarget FindTarget() {
            var targets = provider.GetTargets();
            ITarget nearestTarget = null;
            float minDistance = float.PositiveInfinity;
            foreach (var target in targets) {
                var currentDistance = Vector3.Distance(currentUnit.GetTargetBehaviour().GetPosition(), target.GetTargetBehaviour().GetPosition());
                if (currentDistance < minDistance) {
                    minDistance = currentDistance;
                    nearestTarget = target;
                }
            }
            return nearestTarget;
        }
    }
}