using System;
using Assets.src.data;

namespace Assets.src.models {
    public interface IBuff {
        void Initialize(BaseBuffData data);
        void Apply(IUnit unit);
        void Free();
        Action<IBuff> OnEnd { get; set; }
    }
}