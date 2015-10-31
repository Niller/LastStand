using System;
using Assets.src.data;
using Assets.src.managers;
using Assets.src.views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.src.models {
    public abstract class BaseSpellModel: ISpell, IModel {

        [Inject]
        public IViewModelManager ViewModelManager { get; set; }

        protected ITarget target;

        protected ITarget source;

        public abstract void Apply();

        protected SpellView view;

        protected SpellData baseData;

        public void Initialize(ITarget sourceParam, ITarget targetParam, SpellData data) {
            source = sourceParam;
            target = targetParam;
            baseData = data;
            InitializeData(data);
            InitView();
        }

        protected virtual void InitView() {
            var viewPrefab = GetViewPrefab();
            var viewGo = Object.Instantiate(viewPrefab) as GameObject;
            SetView(viewGo.GetComponent<SpellView>());
            view.Initialize(source.GetTargetBehaviour().GetPosition(), target, baseData);
            view.OnEnd += OnEnd;
        }

        protected void OnEnd() {
            Apply();
        }

        public void SetView(IView viewParam) {
            view = viewParam as SpellView;
        }

        public IView GetView() {
            return view;
        }

        protected abstract GameObject GetViewPrefab();

        protected abstract void InitializeData(SpellData data);
    }
}