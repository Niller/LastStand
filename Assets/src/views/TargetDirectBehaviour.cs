using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using UnityEngine;

namespace Assets.src.views {
    public class TargetDirectBehaviour : IViewTransformBehaviour {

        public Action onEnd;

        protected Transform transform;

        protected ITarget target;

        protected float bulletSpeed;

        protected Vector3 lastTargetPosition;

        protected float totalTime;

        protected float currentTime;

        protected Vector3 startPosition;

        public TargetDirectBehaviour(ITarget targetParam, float bulletSpeedParam) {
            target = targetParam;
            bulletSpeed = bulletSpeedParam;
        }
        
        public void Start(Transform currentTransform, Vector3 startPositionParam, Action endCallback) {
            onEnd = endCallback;
            transform = currentTransform;
            startPosition = startPositionParam;
            lastTargetPosition = target.GetPosition();
            transform.position = startPosition;
            totalTime = (GetTargetPosition() - startPosition).magnitude / bulletSpeed;
            currentTime = 0;
        }

        public void Update(float deltaTime) {
            currentTime += deltaTime;
            lastTargetPosition = GetTargetPosition();
            transform.position = Vector3.Lerp(startPosition, lastTargetPosition, currentTime / totalTime);
            Rotate(lastTargetPosition);
            if (currentTime / totalTime >= 1) {
                onEnd.TryCall();
            }
        }

        protected virtual void Rotate(Vector3 targetPos) {
            transform.LookAt(targetPos);
        }

        protected Vector3 GetTargetPosition() {
            return target.IsUnvailableForAttack() ? lastTargetPosition : target.GetPosition();
        }
    }
}