using System;
using Assets.src.utils;

namespace Assets.src.data {
    [Serializable]
    public class SpellData {
        public float cooldown;
        public SpellTypes type;
    }
}