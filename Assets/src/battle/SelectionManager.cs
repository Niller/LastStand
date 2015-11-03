using System;
using System.Collections.Generic;
using Assets.src.signals;
using Assets.src.utils;
using Assets.src.views;
using UnityEditor;

namespace Assets.src.battle {
    public class SelectionManager : ISelectionManager {

        [Inject]
        public DeselectAllSignal DeselectAllSignal { get; set; }

        private readonly List<ISelectableBehaviour> selectableObjects;

        private readonly List<ISelectableBehaviour> selectedObjects; 

        public Action SelectionChanged { get; set; }

        public SelectionManager() {
            selectableObjects = new List<ISelectableBehaviour>();
            selectedObjects = new List<ISelectableBehaviour>();
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

        public void RegisterSelectableObject(ISelectableBehaviour selectable) {
            selectableObjects.Add(selectable);
        }

        public void UnregisterSelectableObject(ISelectableBehaviour selectable) {
            selectableObjects.Remove(selectable);
        }

        public List<ISelectableBehaviour> GetAllSelectableObjects() {
            return selectableObjects;
        }

        public List<ISelectableBehaviour> GetSelectedObjects() {
            return selectedObjects;
        }

        public void Select(ISelectableBehaviour selectableObject) {
            if (selectableObject == null)
                return;
            selectedObjects.Add(selectableObject);
            selectableObject.Select();
            SelectionChanged.TryCall();
        }

        public void Deselect(ISelectableBehaviour selectableObject) {
            if (selectableObject == null)
                return;
            selectedObjects.Remove(selectableObject);
            SelectionChanged.TryCall();
        }
    }
}