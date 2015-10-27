using Assets.src.models;
using Assets.src.signals;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.mediators {
    public class BaseUnitMediator<T> : BaseTargetMediator<T> where T : IUnit {
        [Inject]
        public UnitView View { get; set; }

        [Inject]
        public OnClickSignal OnClickSignal { get; set; }

        public override void OnRegister() {
            base.OnRegister();
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