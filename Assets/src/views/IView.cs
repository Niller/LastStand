﻿using System;
using UnityEngine;

namespace Assets.src.views {
    public interface IView {
        Vector3 GetPosition();
        GameObject GetGameObject();
        void Destroy();
        Action OnUpdate { get; set; }
    }
}