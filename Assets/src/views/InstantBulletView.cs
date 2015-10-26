using System;
using Assets.src.battle;

namespace Assets.src.views {
    public class InstantBulletView : BulletView {
        public override void Initialize(ITarget targetParam, Action<ITarget> endCallback) {
            base.Initialize(targetParam, endCallback);
            End();
        }
    }
}