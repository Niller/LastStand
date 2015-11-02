using System;

namespace Assets.src.services {
    public interface ICooldownItem {
        int Id { get; }
        float Duration { get; }
        float ElapsedTime { get; }

        Action OnEnd { get; set; }
        float TickInterval { get; }
        Action OnTick { get; set; }

        float GetPCT();
        float GetTimeLeft();
        void AddDuration(float delta);
        float GetDuration();
    }
}