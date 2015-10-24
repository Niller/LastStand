using UnityEngine;

namespace Assets.src.data {
    public abstract class BaseBattleInformer : MonoBehaviour {
        public bool isDefender;
        public abstract BaseBattleData GetBaseBattleData();
    }
}