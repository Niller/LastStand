using System;

namespace Assets.src.views {
    public interface ISelectable {
        IView GetView();
        void Select();
        void Deselect();
        bool IsSelected();
        Action OnSelected { get; set; }
        Action OnDeselected { get; set; }
    }
}