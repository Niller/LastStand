using System;

namespace Assets.src.views {
    public interface ISelectable {
        void Select();
        void Deselect();
        bool IsSelected();
        Action OnSelected { get; set; }
        Action OnDeselected { get; set; }
    }
}