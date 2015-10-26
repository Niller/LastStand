using System.Collections.Generic;
using Assets.src.views;

namespace Assets.src.battle {
    public interface ISelectionManager {
        void RegisterSelectableObject(ISelectable selectable);
        void UnregisterSelectableObject(ISelectable selectable);
        List<ISelectable> GetAllSelectableObjects();
    }
}