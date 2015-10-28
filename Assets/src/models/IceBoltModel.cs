using Assets.src.battle;
using Assets.src.data;
using Assets.src.views;

namespace Assets.src.models {
    public class IceBoltModel : ISpell, IModel {

        private ITarget target;

        private IceBoltData iceBoltData;

        public void InitializeData(IceBoltData data) {
            iceBoltData = data;
        }

        public void Initialize(ITarget targetParam) {
            target = targetParam;
        }

        public void Apply() {
            target.SetDamage(iceBoltData.damage);
        }

        public void SetView(IView view) {
            
        }

        public IView GetView() {
            return null;
        }
    }
}