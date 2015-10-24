using Assets.src.contexts;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class UnitView : BaseView {
        public void SetGoal(Vector3 screenPosition) {
            var ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit rayCastInfo;
            var coll = Physics.Raycast(ray,out rayCastInfo);
            /*foreach (var col in colls)
            {
                var selectableObject = col.collider.gameObject.GetComponent<ISelectableObject>();
                if (selectableObject != null)
                {
                    selectableObject.OnSelect();
                }
            }*/
            var position = rayCastInfo.point;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = position;
        }    
    }
}