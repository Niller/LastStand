using Assets.src.mediators;
using UnityEngine;

namespace Assets.src.views {
    public interface IView {
        IViewModelMediator GetMediator();
        Vector3 GetPosition();
        GameObject GetGameObject();
        void Destroy();
    }
}