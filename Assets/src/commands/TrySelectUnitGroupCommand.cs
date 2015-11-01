using System.Collections.Generic;
using Assets.src.battle;
using Assets.src.signals;
using Assets.src.views;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TrySelectUnitGroupCommand : Command {

        [Inject]
        public Vector3[] Positions { get; set; }

        [Inject]
        public DeselectAllSignal DeselectAllSignal { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        protected Bounds GetViewportBounds(Vector3 screenPosition1, Vector3 screenPosition2) {
            var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
            var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);
            min.z = Camera.main.nearClipPlane;
            max.z = Camera.main.farClipPlane;

            var bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }

        public bool TrySelectInGroup(GameObject gameObject, Vector3[] positions) {
            var viewportBounds =
                GetViewportBounds(positions[0], positions[1]);

            return viewportBounds.Contains(
                Camera.main.WorldToViewportPoint(gameObject.transform.position));

        }

        public override void Execute() {
            if (GameManager.IsControlBlocked())
                return;
            var selectableObjects = SelectionManager.GetAllSelectableObjects();
            List<ISelectableBehaviour> objectsToSelect = new List<ISelectableBehaviour>(); 
            foreach (var selectableObject in selectableObjects) {
                if (TrySelectInGroup(selectableObject.GetView().GetGameObject(), Positions)) {
                    objectsToSelect.Add(selectableObject);
                }
            }
            if (objectsToSelect.Count > 0) {
                DeselectAllSignal.Dispatch();
                foreach (var selectable in objectsToSelect) {
                    SelectionManager.Select(selectable);
                }
            }
        }
    }
}