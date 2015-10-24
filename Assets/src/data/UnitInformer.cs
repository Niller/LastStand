namespace Assets.src.data {
    public class UnitInformer : BaseBattleInformer {
        public UnitData data;

        public override BaseBattleData GetBaseBattleData() {
            return data;
        }
    }
}