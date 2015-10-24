using Assets.src.data;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public interface IView {
        Vector3 GetPosition();
    }

    public abstract class BaseTargetView : View, IView {
        public void DestroyView() {
            Destroy(this);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public BaseBattleData GetData() {
            return GetComponent<BaseBattleInformer>().GetBaseBattleData();
        }
    } 
}