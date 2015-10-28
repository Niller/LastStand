using System;
using Assets.Common.Extensions;
using Assets.src.battle;
using Assets.src.signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class SelectableView : BaseView, ISelectable {

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        public Action OnSelected { get; set; }

        public Action OnDeselected { get; set; }

        protected bool isSelected = false;

        protected override void Start() {
            base.Start();
            RegisterSelectableObject();
        }

        protected virtual void RegisterSelectableObject() {
            SelectionManager.RegisterSelectableObject(this);
        }

        protected virtual void UnregisterSelectableObject() {
            SelectionManager.UnregisterSelectableObject(this);
        }

        public Projector selectableCircle;

        public virtual void Deselect() {
            if (isSelected) {
                isSelected = false;
                OnDeselected.TryCall();
                selectableCircle.enabled = false;
            }
        }

        public bool IsSelected() {
            return isSelected;
        }

        public IView GetView() {
            return this;
        }

        public virtual void Select() {
            isSelected = true;
            OnSelected.TryCall();
            selectableCircle.enabled = true;
        }

        public override void DestroyView() {
            Deselect();
            UnregisterSelectableObject();
            base.DestroyView();
        }
    }
}