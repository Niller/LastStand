using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class BaseView : View, IView {
        public void DestroyView() {
            Destroy(this);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }
    }
}