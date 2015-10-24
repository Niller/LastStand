using System.Collections;
using JetBrains.Annotations;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class SofaView : View, IView {

        public void DestroyView() {
            Destroy(this);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }
    }
}