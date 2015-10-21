using Assets.src.models;

namespace Assets.src.mediators {
    public class SofaMediator : ViewModelMediator<SofaModel> {

        [Inject]
        public SofaView View { get; set; }

        public override void OnRegister() {
            base.OnRegister();
            Model.Mediator = this;
            Model.OnDestroyed += View.DestroyView;
        }

        public override void OnRemove() {
            Model.OnDestroyed -= View.DestroyView;
        }

    }
}