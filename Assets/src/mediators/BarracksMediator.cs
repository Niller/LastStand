using Assets.src.data;
using Assets.src.models;
using Assets.src.views;

namespace Assets.src.mediators {
    public class BarracksMediator : ViewModelMediator<BarracksModel> {
        [Inject]
        public BarracksView View { get; set; }
        
        public BarracksInformer Informer { get; set; }

        public override void OnRegister() {
            Informer = GetComponent<BarracksInformer>();
            base.OnRegister();
            Model.Mediator = this;
            Model.Initialize();
        }

        public override void OnRemove() {
            base.OnRemove();
        }

    }
}