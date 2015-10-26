using Assets.src.contexts;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.src.views {
    public class UnitView : BaseTargetView, INavigationUnit {

        private NavMeshAgent navMeshAgent;
        private NavMeshObstacle navMeshObstacle;

        public NavMeshAgent NavMeshAgent { get { return navMeshAgent = navMeshAgent ?? GetComponent<NavMeshAgent>(); } }
        public NavMeshObstacle NavMeshObstacle { get { return navMeshObstacle = navMeshObstacle ?? GetComponent<NavMeshObstacle>(); } }

        protected override void Awake() {
            base.Awake();
            //NavMeshAgent.avoidancePriority = Random.Range(0,100);
        }

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
            
            //NavMeshAgent agent = GetComponent<NavMeshAgent>();
            //agent.destination = position;
            
        }

        void Update() {
            /*
            if (!NavMeshAgent.enabled)
                return;
            if (!NavMeshAgent.pathPending) {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance) {
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f) {
                        GetComponent<NavMeshObstacle>().enabled = true;
                        NavMeshAgent.enabled = false;
                    }
                }
            }
            */
        }

        public void SetDestination(Vector3 destPosition) {
            NavMeshObstacle.enabled = false;
            NavMeshAgent.enabled = true;
            NavMeshAgent.SetDestination(destPosition);
        }

        public void StopMoving() {
            NavMeshAgent.Stop();
            NavMeshAgent.enabled = false;
            NavMeshObstacle.enabled = true;
        }
    }
}