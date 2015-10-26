using Assets.src.mediators;
using Assets.src.signals;
using Assets.src.views;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TrySelectUnitCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public DeselectAllSignal DeselectAllSignal { get; set; }

        public override void Execute() {
            var ray = Camera.main.ScreenPointToRay(Position);
            var colls = Physics.RaycastAll(ray);
            foreach (var col in colls) {
                var selectableObject = col.collider.gameObject.GetComponent<ISelectable>();
                if (selectableObject != null) {
                    DeselectAllSignal.Dispatch();
                    selectableObject.Select();
                    return;
                }
            }

        }
    }
}