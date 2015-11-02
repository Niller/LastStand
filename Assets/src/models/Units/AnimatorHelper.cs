using Assets.src.views;
using UnityEngine;

namespace ru.pragmatix.orbix.world.units {
    public class AnimatorHelper : IAnimatorHelper {

        private readonly Animator animator;

        private string currentAnimation = null;

        public AnimatorHelper(Animator animatorParam) {
            animator = animatorParam;
        }

        public void SetAnimatorBool(string key, bool value) {
            if (animator != null) {
                if (!value || currentAnimation != key)
                    animator.SetBool(key, value);
                if (value) {
                    currentAnimation = key;
                } else {
                    currentAnimation = null;
                }
            }
        }

        public virtual void PlayAnimation(string animName) {
            if (animator != null) {
                animator.Play(animName, 0, 0);
            }
        }

        public void StopAnimation(string animName) {
            if (animator != null) {
                animator.SetBool(animName, false);
                currentAnimation = null;
            }

        }
        
    }
}