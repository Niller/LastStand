using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.data;
using Assets.src.models;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class SpellView : BaseView {

        public Action OnEnd { get; set; }

        public abstract void Initialize(Vector3 startPositionParam, ITarget targetParam, SpellData data);

        protected void End() {
            OnEnd.TryCall();
            Destroy(gameObject);
        }

    }
}