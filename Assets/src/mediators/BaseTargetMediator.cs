using Assets.src.data;
using Assets.src.models;

namespace Assets.src.mediators {
    public class BaseTargetMediator<T> : ViewModelMediator<T> where T : IModel {

        protected BaseBattleInformer infomer;

        public override void OnRegister() {
            base.OnRegister();
            infomer = GetComponent<BaseBattleInformer>();
        }
    }
}