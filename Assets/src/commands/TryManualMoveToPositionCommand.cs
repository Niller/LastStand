using Assets.src.battle;
using Assets.src.models;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TryManualMoveToPositionCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        public override void Execute() {
            if (GameManager.IsControlBlocked())
                return;
            /*
            foreach (var selectedObject in SelectionManager.GetSelectedObjects()) {
                var unit = selectedObject.GetView().GetMediator().GetModel<IUnit>();
                if (unit.IsManualControl) {
                    var ray = Camera.main.ScreenPointToRay(Position);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"))) {
                        unit.SetMovePoint(new TargetMock(hitInfo.point));
                    }
                }
            }
            */
        }
    }
}