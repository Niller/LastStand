namespace Assets.src.data {
    public class SofaInformer : BaseBattleInformer {
        public BuildingData data;

        public override BaseBattleData GetBaseBattleData() {
            return data;
        }
    }
}