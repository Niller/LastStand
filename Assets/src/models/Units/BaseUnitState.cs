using System;
using Assets.Common.Extensions;
using Assets.src.models;
using strange.extensions.mediation.impl;

namespace ru.pragmatix.orbix.world.units {
    public abstract class BaseUnitState : IUnitState {

        protected IUnit currentUnit;

        protected IAnimatorHelper animatorHelper;

        public virtual void Initialize(IUnit unit, IAnimatorHelper animatorHelper) {
            currentUnit = unit;
            this.animatorHelper = animatorHelper;
        }
        public abstract void Start();
        public abstract void Update();
        public Action OnStop { get; set; }
        public abstract void ForceStop();
        protected virtual void Stop() {
            ForceStop();
            OnStop.TryCall();
        }
    }
}