using Assets.src.battle;
using Assets.src.commands;
using Assets.src.models;
using Assets.src.signals;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.mediators {
    public class SofaMediator : ViewModelMediator<SofaModel> {

        [Inject]
        public SofaView View { get; set; }

        [Inject]
        public RegisterTargetSignal RegisterTargetSignal { get; set; }

        [Inject]
        public UnregisterTargetSignal UnregisterTargetSignal { get; set; }

        public override void OnRegister() {
            base.OnRegister();
            Model.Mediator = this;
            RegisterTargetSignal.Dispatch(Model);
            Model.OnDestroyed += Dispose;
        }

        public override void OnRemove() {
            Model.OnDestroyed -= Dispose;
        }

        protected void Dispose() {
            UnregisterTargetSignal.Dispatch(Model);
            View.DestroyView();
        }

    }
}