using Assets.src.battle;
using Assets.src.data;
using Assets.src.services;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public class MeteorModel : BaseSpellModel {

        private MeteorData meteorData;

        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<MeteorModel>();
        }

        protected override void InitializeData(SpellData data) {
            meteorData = data as MeteorData;
        }

        public override void Apply() {
            var colls = Physics.OverlapSphere(target.GetTargetBehaviour().GetPosition(), meteorData.range);
            foreach (var collider in colls) {
                var targetView = collider.GetComponent<ITargetView>();
                if (targetView != null) {
                    var targetModel = targetView.GetModel<ITarget>();
                    if (targetModel.GetTargetBehaviour().IsDefender != source.GetTargetBehaviour().IsDefender)
                        source.DoDamage(targetModel, meteorData.damage);
                }
            }
        }
    }
}