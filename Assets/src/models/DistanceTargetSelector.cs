using System.Linq;
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
            ITarget nearestTarget = null;
            var targets = provider.GetTargets();
            var sortedTargets = targets.OrderBy(
                t =>
                    Vector3.Distance(currentUnit.GetTargetBehaviour().GetPosition(),
                        t.GetTargetBehaviour().GetPosition())).ToList();
            nearestTarget = sortedTargets.FirstOrDefault(t => t is IUnit) ?? sortedTargets.FirstOrDefault();
            return nearestTarget;
        }
    }
}