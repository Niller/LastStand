using Assets.src.battle;
using Assets.src.data;
using Assets.src.models;
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
            transformBehaviour.Start(transform, new Vector3(targetParam.GetTargetBehaviour().GetPosition().x, targetParam.GetTargetBehaviour().GetPosition().y+10, targetParam.GetTargetBehaviour().GetPosition().z), End);
        }

        protected override void Update() {
            transformBehaviour.Update(Time.deltaTime);
        }
    }
}