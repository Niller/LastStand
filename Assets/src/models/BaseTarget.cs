using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public abstract class BaseTarget : IModel, ITarget {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected IView view;

        protected int currentHealth;

        protected BaseBattleInformer informer;

        public virtual void Initialize(BaseBattleInformer informerParam) {
            informer = informerParam;
            InitializeData();
            BattleManager.RegisterTarget(this);
        }

        public Vector3 GetPosition() {
            return view.GetPosition();
        }

        public void SetView(IView viewParam) {
            view = viewParam;
        }

        protected virtual void InitializeData() {
            
        }

        public void SetDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= 0) {
                Destroy();
            }
        }

        public bool IsDefender { get { return informer.isDefender; } }

        protected void Destroy() {
            BattleManager.UnregisterTarget(this);
            OnDestroyed.TryCall();
        }

        public Action OnDestroyed { get; set; }
    }
}