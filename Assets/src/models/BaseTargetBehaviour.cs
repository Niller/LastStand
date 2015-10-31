using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.views;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.src.models {
    public abstract class BaseTargetBehaviour : ITargetBehaviour {
        [Inject]
        public IBattleManager BattleManager { get; set; }

        protected int currentHealth;

        protected bool isDied;

        protected ITarget parent;

        protected bool isDefender;

        protected BaseBattleData data;

        public float GetVulnerabilityRadius() {
            return data.vulnerabilityRadius;
        }

        public virtual void Initialize(BaseBattleData dataParam, bool isDefenderParam, ITarget parentParam) {
            data = dataParam;
            isDefender = isDefenderParam;
            parent = parentParam;
            InitializeData();
            BattleManager.RegisterTarget(parent);
        }

        public Action OnHPChanged { get; set; }

        public Vector3 GetPosition() {
            return parent.GetView().GetPosition();
        }

        protected virtual void InitializeData() {
            currentHealth = data.health;
        }

        public void SetDamage(int damage) {
            currentHealth -= damage;
            OnHPChanged.TryCall();
            //Debug.Log(currentHealth);
            if (currentHealth <= 0) {
                Destroy();
            }
        }

        public int GetCurrentHP() {
            return currentHealth;
        }

        public int GetMaxHP() {
            return data.health;
        }

        public bool IsDefender { get { return isDefender; } }

        protected virtual void Destroy() {
            isDied = true;
            BattleManager.UnregisterTarget(parent);
            OnDestroyed.TryCall();
        }

        public Action OnDestroyed { get; set; }

        public bool IsUnvailableForAttack() {
            return isDied;
        }

        public abstract bool IsDynamic { get; }
    }
}