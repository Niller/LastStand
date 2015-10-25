using System;
using Assets.src.models;

namespace ru.pragmatix.orbix.world.units {
    public interface IUnitState {
        void Start();
        void Update();
        void Initialize(IUnit unit, IAnimatorHelper animatorHelper);
        Action OnStop { get; set; }
        void ForceStop();
    }
}