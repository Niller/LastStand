using Assets.src.utils;

namespace Assets.src.data {
    public class UnitInformer : BaseBattleInformer {

        public UnitTypes type;

        public UnitData data;

        public override BaseBattleData GetBaseBattleData() {
            return data;
        }
    }
}