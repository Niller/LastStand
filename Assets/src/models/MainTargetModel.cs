using System;
using Assets.src.data;
using Assets.src.views;
using strange.extensions.injector.api;
using UnityEngine;

namespace Assets.src.models {
    public class MainTargetModel : ITarget {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        protected MainTargetView view;

        protected BaseTargetBehaviour baseTargetBehaviour;

        [PostConstruct]
        public void MainTargetModelConstruct() {
            Initialize();
        }

        public void Initialize() {
            baseTargetBehaviour = new BuildingTargetBehaviour();
            InjectionBinder.injector.Inject(baseTargetBehaviour);
            baseTargetBehaviour.Initialize(view.data, true, this);
            baseTargetBehaviour.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed() {
            GetView().Destroy();
        }

        public void SetView(IView viewParam) {
            view = viewParam as MainTargetView;
        }

        public IView GetView() {
            return view;
        }

        public ITargetBehaviour GetTargetBehaviour() {
            return baseTargetBehaviour;
        }
    }
}