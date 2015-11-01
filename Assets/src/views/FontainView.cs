using Assets.src.data;
using UnityEngine;

namespace Assets.src.views {
    public class FontainView : BaseView {
        public Transform spawnPoint;
        public float fontainHealRadius = 15f;
        public float fontainHealTimeStep = 0.5f;
        public int fontainHealPower = 2;
        public HeroData hero;
    }
}