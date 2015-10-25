using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.views;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Assets.src.models {
    public abstract class BaseTargetModel : IModel, ITarget {

        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected ITargetView view;

        protected int currentHealth;

        protected BaseBattleInformer informer;

        protected bool isDied;

        public float GetVulnerabilityRadius() {
            return view.GetVulnerabilityRadius();
        }

        public virtual void Initialize(BaseBattleInformer informerParam) {
            informer = informerParam;
            InitializeData();
            BattleManager.RegisterTarget(this);
        }

        public Vector3 GetPosition() {
            return view.GetPosition();
        }

        public void SetView(IView viewParam) {
            view = (ITargetView)viewParam;
            Assert.AreEqual(false, view==null, "Incorrect view for target object");
        }

        protected virtual void InitializeData() {
            
        }

        public void SetDamage(int damage) {
            currentHealth -= damage;
            Debug.Log(currentHealth);
            if (currentHealth <= 0) {
                Destroy();
            }
        }

        public bool IsDefender { get { return informer.isDefender; } }

        protected virtual void Destroy() {
            isDied = true;
            BattleManager.UnregisterTarget(this);
            OnDestroyed.TryCall();
        }

        public Action OnDestroyed { get; set; }

        public bool IsUnvailableForAttack() {
            return isDied;
        }
    }
}