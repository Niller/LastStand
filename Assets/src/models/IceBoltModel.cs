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
            source.DoDamage(target, iceBoltData.damage);
            if (!target.GetTargetBehaviour().IsUnvailableForAttack()) {
                var unit = target as IUnit;
                if (unit != null) {
                    var buff = new FreezeBuff();
                    InjectionBinder.injector.Inject(buff);
                    buff.Initialize(iceBoltData.buffData);
                    unit.AddBuff(buff);
                }
            }
        }

        protected override GameObject GetViewPrefab() {
            return ViewModelManager.GetView<IceBoltModel>();
        }
    }
}