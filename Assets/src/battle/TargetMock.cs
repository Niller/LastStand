using System;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.battle {
    public class TargetMock : ITarget {

        protected Vector3 position;

        public TargetMock(Vector3 positionParam) {
            position = positionParam;
        }

        public void SetDamage(int damage) {
            
        }

        public bool IsDefender { get { return false; } }
        public Action OnDestroyed { get; set; }

        public Vector3 GetPosition() {
            return position;
        }

        public float GetVulnerabilityRadius() {
            return 1f;
        }

        public void Initialize(BaseBattleInformer informerParam) {
            
        }

        public bool IsUnvailableForAttack() {
            return false;
        }

        public bool IsDynamic { get { return false; } }
    }
}