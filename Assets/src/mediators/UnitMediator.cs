using Assets.src.models;
using Assets.src.signals;
using Assets.src.views;
using strange.extensions.injector.api;
using UnityEngine;

namespace Assets.src.mediators {
    public class UnitMediator : BaseTargetMediator<UnitModel> {
        [Inject]
        public UnitView View { get; set; }

        [Inject]
        public OnClickSignal OnClickSignal { get; set; }


        public override void OnRegister() {
            base.OnRegister();
            Model.Mediator = this;
            //Model.OnDestroyed += Dispose;
            Model.SetView(View);
            Model.Initialize(infomer);
            Model.SetNavUnit(View);
            Model.InitializeStates();
            Model.StartAct();
            Model.OnDestroyed += View.DestroyView;
            OnClickSignal.AddListener(View.SetGoal);
        }

        public override void OnRemove() {
            base.OnRemove();
            Model.OnDestroyed -= View.DestroyView;
            OnClickSignal.RemoveListener(View.SetGoal);
        }

        private void Update() {
            Model.Update();
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            if (Model.GetCurrentTarget() != null && !Model.GetCurrentTarget().IsUnvailableForAttack()) {
                Gizmos.DrawLine(transform.position, Model.GetCurrentTarget().GetPosition());
            }
        }

    }
}