using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.mediators;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.src.models {
    public class SofaModel : BaseTargetModel {

        private SofaData data;

        public SofaMediator Mediator { protected get; set; }

        protected override void InitializeData() {
            base.InitializeData();
            data = informer.GetBaseBattleData() as SofaData;
            //IsDefender = Mediator.Infomer.isDefender;
            if (data != null) {
                currentHealth = data.health;
            } else {
                Debug.LogError("Data isn't valid for this object", Mediator);
            }
        }
    }
}
