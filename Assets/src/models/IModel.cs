using Assets.src.views;
using JetBrains.Annotations;

namespace Assets.src.models {
    public interface IModel {
        void SetView(IView view);
        IView GetView();
    }
}