using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using strange.extensions.mediation.impl;

namespace Assets.src.views {
    public class BulletView : View {

        protected Action<ITarget> onEnd;

        protected ITarget target;

        public virtual void Initialize(ITarget targetParam, Action<ITarget> endCallback) {
            target = targetParam;
            onEnd = endCallback;
        }

        protected void End() {
            onEnd.TryCall(target);
            Destroy(gameObject);
        }
    }
}