using System.Collections.Generic;
using Assets.src.views;

namespace Assets.src.battle {
    public class SelectionManager : ISelectionManager {

        private readonly List<ISelectable> selectableObjects;

        public SelectionManager() {
            selectableObjects = new List<ISelectable>();
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
    }
}