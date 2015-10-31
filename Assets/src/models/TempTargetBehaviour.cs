using System;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.models {
    public class TempTargetBehaviour : ITargetBehaviour {

        protected Vector3 position;

        public void SetDamage(int damage) {
        }

        public bool IsDefender { get { return false; } }
        public Action OnDestroyed { get; set; }
        public Action OnHPChanged { get; set; }

        public Vector3 GetPosition() {
            return position;
        }

        public float GetVulnerabilityRadius() {
            return 0.1f;
        }

        public TempTargetBehaviour(Vector3 positionParam) {
            position = positionParam;
        }

        public void Initialize(BaseBattleData dataParam, bool isDefenderParam, ITarget parentParam) {
        }

        public bool IsUnvailableForAttack() {
            return false;
        }

        public bool IsDynamic { get { return false; } }
        public int GetCurrentHP() {
            throw new NotImplementedException();
        }

        public int GetMaxHP() {
            throw new NotImplementedException();
        }
    }
}