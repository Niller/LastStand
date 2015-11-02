using System;
using Assets.Common.Extensions;
using UnityEngine;

namespace Assets.src.views {
    public class AnimationEventInformer : MonoBehaviour {

        public Action OnAttack { get; set; }
        public Action OnDie { get; set; }

        private void Attack() {
            OnAttack.TryCall();
        }

        private void Die() {
            OnDie.TryCall();
        }
    }
}