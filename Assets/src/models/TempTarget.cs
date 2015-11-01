using Assets.src.views;
using UnityEngine;

namespace Assets.src.models {
    public class TempTarget : ITarget {

        protected ITargetBehaviour targetBehaviour;

        public TempTarget(Vector3 position) {
            targetBehaviour = new TempTargetBehaviour(position);
        }

        public void SetView(IView view) {
            
        }

        public IView GetView() {
            return null;
        }

        public ITargetBehaviour GetTargetBehaviour() {
            return targetBehaviour;
        }
    }
}