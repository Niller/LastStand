using Assets.src.views;

namespace ru.pragmatix.orbix.world.units {
    public interface IAnimatorHelper {
        void SetAnimatorBool(string key, bool value);
        void PlayAnimation(string animName);
        void StopAnimation(string animName);
        AnimationEventInformer GetAnimationEventInformer();
    }
}