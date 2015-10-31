using System.Collections.Generic;
using Assets.src.data;
using Assets.src.models;

namespace Assets.src.battle {
    public interface IBattleManager {
        void Initialize();
        void RegisterTarget(ITarget target);
        void UnregisterTarget(ITarget target);
        void RegisterSpawner(ISpawner spawner);
        List<ITarget> GetDefenders();
        List<ITarget> GetAttackers();
        void RegisterFontaion(FontainModel fontain);
        HeroData GetCurrentHeroData();
        void SaveCurrentHeroData(HeroData data);

    }
}