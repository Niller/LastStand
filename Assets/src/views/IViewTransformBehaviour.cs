using System;
using UnityEngine;

namespace Assets.src.views {
    public interface IViewTransformBehaviour {
        void Start(Transform currentTransform, Vector3 startPosition, Action endCallback);
        void Update(float deltaTime);
    }
}