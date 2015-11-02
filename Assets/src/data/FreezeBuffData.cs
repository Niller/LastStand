using System;

namespace Assets.src.data {
    [Serializable]
    public class FreezeBuffData : BaseBuffData {
        public float duration;
        public float movementCoef;
        public float attackSpeedCoef;
        
    }
}