using System;
using Assets.src.battle;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.models {
    public interface ITargetBehaviour {
        void SetDamage(int damage);
        bool IsDefender { get; }
        Action OnDestroyed { get; set; }
        Vector3 GetPosition();
        float GetVulnerabilityRadius();
        void Initialize(BaseBattleData dataParam, bool isDefenderParam, ITarget1 parentParam);
        bool IsUnvailableForAttack();
        bool IsDynamic { get; }
    }
}