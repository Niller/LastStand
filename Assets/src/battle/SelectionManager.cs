using System.Collections.Generic;
using Assets.src.signals;
using Assets.src.views;
using UnityEditor;

namespace Assets.src.battle {
    public class SelectionManager : ISelectionManager {

        [Inject]
        public DeselectAllSignal DeselectAllSignal { get; set; }

        private readonly List<ISelectable> selectableObjects;

        private readonly List<ISelectable> selectedObjects; 

        public SelectionManager() {
            selectableObjects = new List<ISelectable>();
            selectedObjects = new List<ISelectable>();
        }

        [PostConstruct]
        public void Initialize() {
            DeselectAllSignal.AddListener(OnDeselectAll);
        }

        private void OnDeselectAll() {
            foreach (var selectedObject in selectedObjects) {
                selectedObject.Deselect();
            }
            selectedObjects.Clear();
        }

        public void RegisterSelectableObject(ISelectable selectable) {
            selectableObjects.Add(selectable);
        }

        public void UnregisterSelectableObject(ISelectable selectable) {
            selectableObjects.Remove(selectable);
        }

        public List<ISelectable> GetAllSelectableObjects() {
            return selectableObjects;
        }

        public List<ISelectable> GetSelectedObjects() {
            return selectedObjects;
        }

        public void Select(ISelectable selectableObject) {
            selectedObjects.Add(selectableObject);
            selectableObject.Select();
        }

        public void Deselect(ISelectable selectableObject) {
            selectedObjects.Remove(selectableObject);
        }
    }
}