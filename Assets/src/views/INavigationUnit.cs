using System;
using UnityEngine;

namespace Assets.src.views {
    public interface INavigationUnit {
        void SetDestination(Vector3 destPosition);
        void StopMoving();
    }
}