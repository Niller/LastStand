using System;
using Assets.src.models;
using Assets.src.utils;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class BaseView : View, IView {

        protected IModel model;

        public Action OnUpdate { get; set; }

        public T GetModel<T>() where T : class, IModel {
            return model as T;
        }

        public void SetModel(IModel modelParam) {
            if (model == null) {
                model = modelParam;
            } else {
                Debug.LogError("Try to reset model for view", this);
            }
        }

        public virtual void Destroy() {
            Destroy(gameObject);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public Vector3 GetForward() {
            return transform.forward;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }

        protected virtual void Update() {
            OnUpdate.TryCall();
        }
    }
}