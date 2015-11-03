using System;
using Assets.src.models;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.views {
    public class BaseUnitView : BaseView, INavigationUnit, ITargetView {

        protected ISelectableBehaviour selectableBehaviour;

        public ISelectableBehaviour SelectableBehaviour { get { return selectableBehaviour = selectableBehaviour ?? GetComponent<ISelectableBehaviour>(); } }

        public Animator animator;

        protected NavMeshAgent navMeshAgent;
        protected NavMeshObstacle navMeshObstacle;

        protected NavMeshAgent NavMeshAgent { get { return navMeshAgent = navMeshAgent ?? GetComponent<NavMeshAgent>(); } }
        protected NavMeshObstacle NavMeshObstacle { get { return navMeshObstacle = navMeshObstacle ?? GetComponent<NavMeshObstacle>(); } }

        public void RotateToPosition(Vector3 position) {
            transform.LookAt(position);
        }

        protected override void Update() {
            base.Update();
            NavMeshAgent.speed = GetModel<IUnit>().GetUnitData().movementSpeed;
        }

        public void SetDestination(Vector3 destPosition) {
            
            NavMeshObstacle.enabled = false;
            NavMeshAgent.enabled = true;
            NavMeshAgent.Resume();
            NavMeshAgent.SetDestination(destPosition);
        }

        public void StopMoving() {
            NavMeshAgent.Stop();
            //NavMeshAgent.enabled = false;
            //NavMeshObstacle.enabled = true;
        }
    }
}