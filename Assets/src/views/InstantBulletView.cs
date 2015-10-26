using System;
using Assets.src.battle;
using UnityEngine;

namespace Assets.src.views {
    public class InstantBulletView : BulletView {
        public override void Initialize(Vector3 startPosition, ITarget targetParam, Action<ITarget> endCallback) {
            base.Initialize(startPosition, targetParam, endCallback);
            End();
        }
    }
}