using Assets.src.battle;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.views {
    public class IceBoltView : SpellView {
        public float bulletSpeed;

        protected IViewTransformBehaviour transformBehaviour;

        protected ITarget target;

        public IceBoltData data;

        public IceBoltData GetData() {
            return data;
        }

        public ITarget GetTarget() {
            return target;
        }

        public override void Initialize(Vector3 startPositionParam, ITarget targetParam, SpellData dataParam) {
            data = dataParam as IceBoltData;
            target = targetParam;
            transformBehaviour = new TargetDirectBehaviour(targetParam, bulletSpeed);
            transformBehaviour.Start(transform, startPositionParam, End);
        }

        private void Update() {
            transformBehaviour.Update(Time.deltaTime);
        }
    }
}