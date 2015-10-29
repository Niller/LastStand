using Assets.src.battle;
using Assets.src.data;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public class MeteorModel : ISpell, IModel {
        private ITarget target;

        private MeteorData meteorData;

        public void InitializeData(MeteorData data) {
            meteorData = data;
        }

        public void Initialize(ITarget targetParam) {
            target = targetParam;
        }

        public void Apply() {
            var colls = Physics.OverlapSphere(target.GetPosition(), meteorData.range);
            foreach (var collider in colls) {
                var targetView = collider.GetComponent<ITargetView>();
                if (targetView != null) {
                    var model = targetView.GetMediator().GetModel<BaseTargetModel>();
                    model.SetDamage(meteorData.damage);
                }
            }
        }

        public void SetView(IView view) {

        }

        public IView GetView() {
            return null;
        }

        public void Initialize() {
            
        }
    }
}