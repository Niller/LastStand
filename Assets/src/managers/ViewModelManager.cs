using System;
using System.Collections;
using System.Collections.Generic;
using Assets.src.models;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.managers {
    public interface IViewModelManager {
        GameObject GetView<T>() where T: IModel;
        Type GetModel(Type viewType);
    }

    public class ViewModelManager : IViewModelManager {

        protected Dictionary<Type,string> viewPaths = new Dictionary<Type, string>() {
            
        }; 

        protected Dictionary<string, GameObject> cachedView = new Dictionary<string, GameObject>();
        
        protected Dictionary<Type, Type> modelsByView = new Dictionary<Type, Type>() {
            {typeof(MainTargetView), typeof(MainTargetModel) },
            {typeof(UnitSpawnerView), typeof(UnitSpawnerModel) }
        };

        public Type GetModel(Type viewType) {
            return modelsByView[viewType];
        }

        public GameObject GetView<T>() where T : IModel {
            var goPath = viewPaths[typeof (T)];
            if (!cachedView.ContainsKey(goPath)) {
                var go = Resources.Load(goPath) as GameObject;
                cachedView.Add(goPath, go);
            } 
            return cachedView[goPath];
        }
    }
}