using System;
using Assets.src.battle;
using UnityEngine;

namespace Assets.src.views {
    public class TargetDirectBulletView : BulletView {

        public float bulletSpeed;

        protected Vector3 lastTargetPosition;

        protected float totalTime;

        protected float currentTime;

        protected Vector3 startPosition;

        public override void Initialize(Vector3 startPositionParam, ITarget targetParam, Action<ITarget> endCallback) {
            base.Initialize(startPositionParam, targetParam, endCallback);
            startPosition = startPositionParam;
            lastTargetPosition = targetParam.GetPosition();
            transform.position = startPosition;
            totalTime = (GetTargetPosition() - startPosition).magnitude/bulletSpeed;
            //Debug.LogError(totalTime);
            currentTime = 0;
        }

        protected virtual void Rotate(Vector3 targetPos) {
            transform.LookAt(targetPos);
        }

        private void Update() {
            currentTime += Time.deltaTime;
            lastTargetPosition = GetTargetPosition();
            transform.position = Vector3.Lerp(startPosition, lastTargetPosition, currentTime/totalTime);
            //Rotate(lastTargetPosition);
            if (currentTime/totalTime >= 1) {
                End();
            }
        }

        protected Vector3 GetTargetPosition() {
            return target.IsUnvailableForAttack() ? lastTargetPosition : target.GetPosition();
        }
    }
}