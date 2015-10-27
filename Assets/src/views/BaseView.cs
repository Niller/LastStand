using Assets.src.mediators;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class BaseView : View, IView {

        protected IViewModelMediator mediator;

        public IViewModelMediator Mediator { get { return mediator = mediator ?? GetComponent<IViewModelMediator>(); } }

        public virtual void DestroyView() {
            Destroy(gameObject);
        }

        public IViewModelMediator GetMediator() {
            return Mediator;
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }
    }
}