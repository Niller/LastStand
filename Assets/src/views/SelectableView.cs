using Assets.src.battle;
using Assets.src.signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class SelectableView : View, ISelectable {

        [Inject]
        public ISelectionManager SelectionManager { get; set; }
        
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

        public void Deselect() {
            selectableCircle.enabled = false;
        }

        public void Select() {
            selectableCircle.enabled = true;
        }

        protected override void OnDestroy() {
            SelectionManager.Deselect(this);
            UnregisterSelectableObject();
            base.OnDestroy();
        }


    }
}