using Assets.src.models;
using Assets.src.views;

namespace Assets.src.mediators {
    class IceBoltMediator : SpellMediator<IceBoltModel> {

        [Inject]
        public IceBoltView View { get; set; }

        public override void OnRegister() {
            base.OnRegister();
            View.OnEnd += Model.Apply;
            Model.Initialize(View.GetTarget());
            Model.InitializeData(View.GetData()); 
        }

        public override void OnRemove() {
            base.OnRemove();
            View.OnEnd -= Model.Apply;
        }
    }
}