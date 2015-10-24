using System.Collections.Generic;

namespace Assets.src.battle {
    public interface IBattleManager {
        void Initialize();
        void RegisterTarget(ITarget target);
        void UnregisterTarget(ITarget target);
        List<ITarget> GetDefenders();
        List<ITarget> GetAttackers();
    }
}