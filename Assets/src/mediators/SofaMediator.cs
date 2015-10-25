using Assets.src.battle;
using Assets.src.commands;
using Assets.src.models;
using Assets.src.signals;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.mediators {
    public class SofaMediator : BaseTargetMediator<SofaModel> {

        [Inject]
        public SofaView View { get; set; }

        public override void OnRegister() {
            base.OnRegister();
            Model.Mediator = this;
            Model.OnDestroyed += Dispose;
            Model.SetView(View);
            Model.Initialize(infomer);
        }

        public override void OnRemove() {
            base.OnRemove();
            Model.OnDestroyed -= Dispose;
        }

        protected void Dispose() {
            View.DestroyView();
        }

    }
}