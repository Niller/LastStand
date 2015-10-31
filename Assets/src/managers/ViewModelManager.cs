using System;
using System.Collections;
using System.Collections.Generic;
using Assets.src.models;
using Assets.src.views;
using UnityEngine;

namespace Assets.src.managers {
    public interface IViewModelManager {
        GameObject GetView<T>() where T: IModel;
        GameObject GetView<T>(int index) where T : IModel;
        Type GetModel(Type viewType);
    }

    public class ViewModelManager : IViewModelManager {

        protected Dictionary<Type,List<string>> viewPaths = new Dictionary<Type, List<string>>() {
            {typeof(UnitModel), new List<string>() { "prefabs/EnemyMeleeUnit", "prefabs/MinionMeleeUnit", "prefabs/EnemyRangeUnit" , "prefabs/MinionRangeUnit" } }
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
            var goPath = viewPaths[typeof (T)][0];
            if (!cachedView.ContainsKey(goPath)) {
                var go = Resources.Load(goPath) as GameObject;
                cachedView.Add(goPath, go);
            } 
            return cachedView[goPath];
        }

        public GameObject GetView<T>(int index) where T : IModel {
            var goPath = viewPaths[typeof(T)][index];
            if (!cachedView.ContainsKey(goPath)) {
                var go = Resources.Load(goPath) as GameObject;
                cachedView.Add(goPath, go);
            }
            return cachedView[goPath];
        }
    }
}