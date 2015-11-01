using System;
using System.Security.Policy;
using strange.extensions.injector.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Assets.src.gui {
    public class NDataInitializer : View {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        public NguiRootContext view;

        protected override void Awake() {
            base.Awake();
            var rootContext = new GUIRootContext();
            InjectionBinder.injector.Inject(rootContext);
            view.SetContext(rootContext);
            rootContext.Initialize();
        }

    }
}
