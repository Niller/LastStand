using System;
using Assets.Common.Extensions;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class BaseView : View, IView {

        public Action OnUpdate { get; set; }

        public virtual void Destroy() {
            Destroy(gameObject);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }

        protected virtual void Update() {
            OnUpdate.TryCall();
        }
    }
}