using Assets.src.battle;
using Assets.src.models;
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
                
                priorityTarget = targetView.GetModel<ITarget>();
            }
            if (priorityTarget != null) {
                foreach (var selectedObject in selectedObjects) {
                    var unit = selectedObject.GetView().GetModel<IUnit>();
                    if (unit.GetTargetBehaviour().IsDefender != priorityTarget.GetTargetBehaviour().IsDefender)
                        selectedObject.GetView().GetModel<IUnit>().SetPriorityTarget(priorityTarget, true);                    
                }
            }
            
        }


    }
}