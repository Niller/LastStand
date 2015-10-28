using Assets.src.models;
using Assets.src.views;

namespace Assets.src.mediators {
    class MeteorMediator : SpellMediator<MeteorModel> {

        [Inject]
        public MeteorView View { get; set; }

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