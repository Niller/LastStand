using System;
using Assets.src.battle;
using Assets.src.signals;
using Assets.src.utils;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class SelectableBehaviour : View, ISelectableBehaviour {

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        public Action OnSelected { get; set; }

        public Action OnDeselected { get; set; }

        protected bool isSelected = false;

        protected IView parent;

        public Projector selectableCircle;

        protected override void Start() {
            base.Start();
            parent = GetComponent<IView>();
            RegisterSelectableObject();
        }

        protected virtual void RegisterSelectableObject() {
            SelectionManager.RegisterSelectableObject(this);
        }

        protected virtual void UnregisterSelectableObject() {
            SelectionManager.UnregisterSelectableObject(this);
        }

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
            return parent;
        }

        public virtual void Select() {
            isSelected = true;
            OnSelected.TryCall();
            selectableCircle.enabled = true;
        }

        protected override void OnDestroy() {
            Deselect();
            UnregisterSelectableObject();
            base.OnDestroy();
        }
    }
}