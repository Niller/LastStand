using System;
using System.Collections;
using Assets.src.models;
using JetBrains.Annotations;
using strange.extensions.injector.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.mediators {
    public abstract class ViewModelMediator<T> : EventMediator, IViewModelMediator where T : IModel {

        public T Model { get; set; }

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        public override void OnRegister() {
            base.OnRegister();
            Model = Activator.CreateInstance<T>();
            InjectionBinder.injector.Inject(Model);
        }

        public T1 GetModel<T1>() where T1 : class, IModel  {
            var model = Model as T1;
            if (model == null)
                throw new InvalidCastException();
            return model;
        }
    }
}