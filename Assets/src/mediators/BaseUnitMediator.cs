using Assets.src.models;
using Assets.src.signals;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.mediators {
    public abstract class BaseUnitMediator<T> : BaseTargetMediator<T> where T : IUnit {
        

        [Inject]
        public OnClickSignal OnClickSignal { get; set; }

        public override void OnRegister() {
            base.OnRegister();
            Model.SetView(GetUnitView());
            Model.Initialize(infomer);
            Model.SetNavUnit(GetUnitView());
            Model.InitializeStates();
            Model.StartAct();
            Model.OnDestroyed += GetUnitView().Destroy;
            OnClickSignal.AddListener(GetUnitView().SetGoal);
        }

        public override void OnRemove() {
            base.OnRemove();
            Model.OnDestroyed -= GetUnitView().Destroy;
            OnClickSignal.RemoveListener(GetUnitView().SetGoal);
        }

        private void Update() {
            Model.Update();
        }

        protected abstract BaseUnitView GetUnitView();


        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            if (Model.GetCurrentTarget() != null && !Model.GetCurrentTarget().IsUnvailableForAttack()) {
                Gizmos.DrawLine(transform.position, Model.GetCurrentTarget().GetPosition());
            }
        }
    }
}