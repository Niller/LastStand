﻿namespace Assets.src.data {
    public class SofaInformer : BaseBattleInformer {
        public SofaData data;

        public override BaseBattleData GetBaseBattleData() {
            return data;
        }
    }
}