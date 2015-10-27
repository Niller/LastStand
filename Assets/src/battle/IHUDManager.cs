using Assets.src.utils;
using UnityEngine;

namespace Assets.src.battle {
    public interface IHUDManager {
        void AddHUD(GameObject go, HudTypes type);
        void RemoveHUD(GameObject go, HudTypes type);
    }
}