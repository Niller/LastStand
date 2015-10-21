using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.mediators;

namespace Assets.src.models {
    public class SofaModel : IModel, ITarget {

        public Action OnDestroyed { get; set; }

        private SofaData data;

        private int currentHealth;

        public void SetDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= 0) {
                Destroy();
            }
        }

        public bool IsDefender { get { return true; } }

        private void Destroy() {
            OnDestroyed.TryCall();
        }

        public void InitializeData(SofaData dataParam) {    
            data = dataParam;
            currentHealth = data.health;
        } 

        public SofaMediator Mediator { protected get; set; }
    }
}
