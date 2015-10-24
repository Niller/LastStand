using Assets.src.models;
using Assets.src.signals;
using Assets.src.views;
using strange.extensions.injector.api;

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
            Model.Initialize(infomer);
            OnClickSignal.AddListener(View.SetGoal);
        }

        public override void OnRemove() {
            base.OnRemove();
            OnClickSignal.RemoveListener(View.SetGoal);
        }

    }
}