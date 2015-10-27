using System;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.battle {
    public interface ITarget {
        void SetDamage(int damage);
        bool IsDefender { get; }
        Action OnDestroyed { get; set; }
        Vector3 GetPosition();
        float GetVulnerabilityRadius();
        void Initialize(BaseBattleInformer informerParam);
        bool IsUnvailableForAttack();
        bool IsDynamic { get; }
       
    }
}
