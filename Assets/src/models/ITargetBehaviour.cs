using System;
using Assets.src.battle;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.models {
    public interface ITargetBehaviour {
        void SetDamage(int damage);
        bool IsDefender { get; }
        Action OnDestroyed { get; set; }
        Action OnHPChanged { get; set; }
        Vector3 GetPosition();
        float GetVulnerabilityRadius();
        void Initialize(BaseBattleData dataParam, bool isDefenderParam, ITarget parentParam);
        bool IsUnvailableForAttack();
        bool IsDynamic { get; }
        int GetCurrentHP();
        int GetMaxHP();
    }
}