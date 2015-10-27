using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class BaseView : View, IView {
        public virtual void DestroyView() {
            Destroy(gameObject);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }
    }
}