
using System;
using strange.framework.api;
using UnityEngine;

namespace strange.examples.strangerocks
{
	public class ResourceInstanceProvider : IInstanceProvider
	{
		//The GameObject instantiated from the prefab
		GameObject prototype;

		private string resourceName;
		private int layer;
		private int id = 0;

		public ResourceInstanceProvider(string name, int layer) {
			resourceName = name;
			this.layer = layer;
		}

		public T GetInstance<T> () {
			object instance = GetInstance (typeof(T));
			T retv = (T) instance;
			return retv;
		}

		public object GetInstance (Type key) {
			if (prototype == null) {
				prototype = Resources.Load<GameObject> (resourceName);
				prototype.transform.localScale = Vector3.one;
			}

			GameObject go = GameObject.Instantiate (prototype) as GameObject;
			go.name = resourceName + "_" + id++;

			return go;
		}
	}
}

