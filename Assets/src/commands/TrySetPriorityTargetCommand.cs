using Assets.src.battle;
using Assets.src.mediators;
using Assets.src.views;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TrySetPriorityTargetCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        public override void Execute() {
            var selectedObjects = SelectionManager.GetSelectedObjects();
            var ray = Camera.main.ScreenPointToRay(Position);
            var colls = Physics.RaycastAll(ray);
            ITarget priorityTarget = null;
            foreach (var col in colls) {
                var targetView = col.collider.gameObject.GetComponent<ITargetView>();
                if (targetView == null)
                    continue;
                var monoBehaviour = targetView as MonoBehaviour;
                var target = monoBehaviour.GetComponent<IViewModelMediator>().GetModel() as ITarget;
                if (target != null) {
                    priorityTarget = target;
                }
            }
            if (priorityTarget != null) {
                foreach (var selectedObject in selectedObjects) {
                    var monoBehaviour = selectedObject as MonoBehaviour;
                    var unitMediator = monoBehaviour.GetComponent<UnitMediator>();
                    if (unitMediator != null) {
                        unitMediator.Model.SetPriorityTarget(priorityTarget, true);
                    }
                }
            }
        }


    }
}