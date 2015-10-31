using System;
using Assets.src.managers;
using Assets.src.models;
using strange.extensions.injector.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class CreateModelByView : View {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        [Inject]
        public IViewModelManager ViewModelManager { get; set; }

        protected override void Start() {
            base.Start();
            var view = GetComponent<IView>();
            var modelType = ViewModelManager.GetModel(view.GetType());
            var modelInstance = Activator.CreateInstance(modelType) as IModel;
            if (modelInstance != null) {
                modelInstance.SetView(view);
                view.SetModel(modelInstance);
                InjectionBinder.injector.Inject(modelInstance);
            } else {
                Debug.LogError("Model hasn't valid type");
            }
            Destroy(this);
        }
    }
}