using Assets.src.battle;
using Assets.src.data;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.models {
    public class IceBoltModel : BaseSpellModel {

        private IceBoltData iceBoltData;
        
        protected override void InitializeData(SpellData data) {
            iceBoltData = data as IceBoltData;
        }

        public override void Apply() {
            target.GetTargetBehaviour().SetDamage(iceBoltData.damage);
        }

        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<IceBoltModel>();
        }
    }
}