using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class AreaSelectorView : View {

        protected Projector projector;

        protected override void Awake() {
            base.Awake();
            projector = GetComponent<Projector>();
        }

        public void SetRadius(float radius) {
            projector.orthographicSize = radius*2f;
        }

        void Update() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"))) {
                transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
            }
        }

        public void Activate() {
            projector.enabled = true;
        }

        public void Deactive() {
            projector.enabled = false;
        }

    }
}