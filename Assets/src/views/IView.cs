using System;
using Assets.src.models;
using UnityEngine;

namespace Assets.src.views {
    public interface IView {
        Vector3 GetPosition();
        GameObject GetGameObject();
        void Destroy();
        Action OnUpdate { get; set; }
        T GetModel<T>() where T : class, IModel;
        void SetModel(IModel model);
    }
}