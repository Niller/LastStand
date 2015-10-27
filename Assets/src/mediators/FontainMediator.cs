using Assets.src.data;
using Assets.src.models;
using Assets.src.views;

namespace Assets.src.mediators {
    public class FontainMediator : ViewModelMediator<FontainModel> {
        [Inject]
        public FontainView View { get; set; }

        public FontainInformer Informer { get; set; }

        public override void OnRegister() {
            Informer = GetComponent<FontainInformer>();
            base.OnRegister();
            Model.Mediator = this;
            Model.Initialize();
        }
    }
}