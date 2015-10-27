using Assets.src.data;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class BaseTargetView : SelectableView, ITargetView {

        public SphereCollider vulnerabilityCollider;

        public float GetVulnerabilityRadius() {
            return vulnerabilityCollider.radius;
        }

        public BaseBattleData GetData() {
            return GetComponent<BaseBattleInformer>().GetBaseBattleData();
        }

        protected override void RegisterSelectableObject() {
            if (GetComponent<BaseBattleInformer>().isDefender) {
                base.RegisterSelectableObject();
            }
        }

        protected override void UnregisterSelectableObject() {
            if (GetComponent<BaseBattleInformer>().isDefender) {
                base.UnregisterSelectableObject();
            }
        }
    }
}