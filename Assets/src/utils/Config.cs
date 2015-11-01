using Assets.src.data;
using UnityEngine;

namespace Assets.src.utils {
    public class Config : MonoBehaviour {

        public IceBoltData[] iceBoltLevelsData;

        public MeteorData[] meteorLevelsData;

        public int heroMaxLevel = 10;

        public int heroXpStep = 100;

        public float heroRespawnTime = 20f;

        public int roundCount = 5;

        public int roundEnemiesCount = 40;

        public float timeBeforeRoundStart = 10f;

    }
}
