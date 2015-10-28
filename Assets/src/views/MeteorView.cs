using Assets.src.battle;
using Assets.src.data;
using UnityEngine;

namespace Assets.src.views {
    public class MeteorView : SpellView {
        public float bulletSpeed;

        protected IViewTransformBehaviour transformBehaviour;

        protected ITarget target;

        public MeteorData data;

        public MeteorData GetData() {
            return data;
        }

        public ITarget GetTarget() {
            return target;
        }

        public override void Initialize(Vector3 startPositionParam, ITarget targetParam, SpellData dataParam) {
            data = dataParam as MeteorData;
            target = targetParam;
            transformBehaviour = new TargetDirectBehaviour(targetParam, bulletSpeed);
            transformBehaviour.Start(transform, new Vector3(targetParam.GetPosition().x, targetParam.GetPosition().y+10, targetParam.GetPosition().z), End);
        }

        private void Update() {
            transformBehaviour.Update(Time.deltaTime);
        }
    }
}