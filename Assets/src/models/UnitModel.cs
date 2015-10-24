using System;
using Assets.src.data;
using Assets.src.mediators;
using strange.extensions.injector.api;
using UnityEngine;

namespace Assets.src.models {
    public abstract class BaseUnitModel<TTargetSelector,TTargetProvider> : BaseTarget 
        where TTargetSelector : ITargetSelector 
        where TTargetProvider : ITargetProvider {

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }

        protected TTargetSelector targetSelector;

        protected TTargetProvider targetProvider;

        public UnitMediator Mediator { protected get; set; }

        protected UnitData data;

        public override void Initialize(BaseBattleInformer informerParam) {
            base.Initialize(informerParam);
            targetProvider = Activator.CreateInstance<TTargetProvider>();
            InjectionBinder.injector.Inject(targetProvider);
            targetProvider.SetCurrentUnit(this);
            targetSelector = Activator.CreateInstance<TTargetSelector>();
            InjectionBinder.injector.Inject(targetSelector);
            targetSelector.SetProvider(targetProvider);
            targetSelector.SetCurrentUnit(this);
            Debug.Log(targetProvider.GetTargets().Count);
        }

        protected override void InitializeData() {
            base.InitializeData();
            data = informer.GetBaseBattleData() as UnitData;
            if (data != null) {
                currentHealth = data.health;
            } else {
                Debug.LogError("Data isn't valid for this object", Mediator);
            }
        }


    }

    public class UnitModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {

        
    }
}