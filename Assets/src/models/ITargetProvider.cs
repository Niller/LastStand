using System.Collections.Generic;
using Assets.src.battle;

namespace Assets.src.models {
    public interface ITargetProvider {
        void SetCurrentUnit(ITarget currentUnitParam);
        List<ITarget> GetTargets();
    }
}