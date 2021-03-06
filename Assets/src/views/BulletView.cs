using System;
using Assets.src.battle;
using Assets.src.models;
using Assets.src.utils;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class BulletView : View {
            
        protected Action<ITarget> onEnd;

        protected ITarget target;

        public virtual void Initialize(Vector3 startPosition, ITarget targetParam, Action<ITarget> endCallback) {
            target = targetParam;
            onEnd = endCallback;
        }

        protected void End() {
            onEnd.TryCall(target);
            Destroy(gameObject);
        }
    }
}