using System;

namespace Assets.src.services {
    public interface ICooldownItem {
        int Id { get; }
        float Duration { get; }
        float ElapsedTime { get; }
        bool Test { get; set; }

        Action OnTick { get; set; }
        Action OnEnd { get; }
        float TickInterval { get; }

        float GetPCT();
        float GetTimeLeft();
        void AddDuration(float delta);
        float GetDuration();
    }
}