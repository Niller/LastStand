using Assets.src.contexts;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.services {
    public interface IGameResourcesService : IService {
        Texture2D GetTargetCursor();
        AreaSelectorView GetAreaSelector();
    }
}