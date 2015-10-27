using Assets.src.battle;
using Assets.src.mediators;
using Assets.src.models;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TryManualMoveToPositionCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        public override void Execute() {
            foreach (var selectedObject in SelectionManager.GetSelectedObjects()) {
                var monoBehaviour = selectedObject as MonoBehaviour;
                if (monoBehaviour == null)
                    return;
                var unitMediator = monoBehaviour.GetComponent<IViewModelMediator>();
                if (unitMediator != null) {
                    var unit = unitMediator.GetModel() as UnitModel;
                    if (unit != null) {
                        if (unit.IsManualControl) {
                            var ray = Camera.main.ScreenPointToRay(Position);
                            RaycastHit hitInfo;
                            if (Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"))) {
                                unit.SetMovePoint(new TargetMock(hitInfo.point));
                            }
                        }
                    }
                }
            }
        }
    }
}