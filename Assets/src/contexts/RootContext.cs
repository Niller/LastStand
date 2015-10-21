using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace Assets.src.contexts {
    public class RootContext : ContextView {

        private List<IService> services = new List<IService>(); 

        void Awake () {
            context = new GameContext(this);
        }

        public void AddService(IService service) {
            services.Add(service);
        }

        void Update() {
            foreach (var service in services) {
                service.OnUpdate();
            }
        }
    }
}