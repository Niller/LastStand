using System;
using System.Collections.Generic;
using Assets.src.views;

namespace Assets.src.battle {
    public interface ISelectionManager {
        void RegisterSelectableObject(ISelectableBehaviour selectable);
        void UnregisterSelectableObject(ISelectableBehaviour selectable);
        List<ISelectableBehaviour> GetAllSelectableObjects();
        List<ISelectableBehaviour> GetSelectedObjects();
        void Select(ISelectableBehaviour selectableObject);
        void Deselect(ISelectableBehaviour selectableObject);
        Action SelectionChanged { get; set; }
    }
}