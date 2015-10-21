using System;
using System.Collections;
using Assets.src.models;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.mediators {
    public abstract class ViewModelMediator<T> : EventMediator where T : IModel {

        public T Model { get; set; }

        public override void OnRegister() {
            Model = Activator.CreateInstance<T>();
        }
    }
}