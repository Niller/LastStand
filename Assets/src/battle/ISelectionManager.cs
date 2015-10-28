using System;
using System.Collections.Generic;
using Assets.src.views;

namespace Assets.src.battle {
    public interface ISelectionManager {
        void RegisterSelectableObject(ISelectable selectable);
        void UnregisterSelectableObject(ISelectable selectable);
        List<ISelectable> GetAllSelectableObjects();
        List<ISelectable> GetSelectedObjects();
        void Select(ISelectable selectableObject);
        void Deselect(ISelectable selectableObject);
        Action SelectionChanged { get; set; }
    }
}