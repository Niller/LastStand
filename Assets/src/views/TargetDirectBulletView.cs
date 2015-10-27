using System;
using Assets.src.battle;
using UnityEngine;

namespace Assets.src.views {
    public class TargetDirectBulletView : BulletView {

        public float bulletSpeed;

        protected IViewTransformBehaviour transformBehaviour;

        public override void Initialize(Vector3 startPositionParam, ITarget targetParam, Action<ITarget> endCallback) {
            base.Initialize(startPositionParam, targetParam, endCallback);
            transformBehaviour = new TargetDirectBehaviour(targetParam, bulletSpeed);
            transformBehaviour.Start(transform, startPositionParam, End);
        }

        private void Update() {
            transformBehaviour.Update(Time.deltaTime);
        }
    }
}