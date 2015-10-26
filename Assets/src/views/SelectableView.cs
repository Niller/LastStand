using Assets.src.battle;
using Assets.src.signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public abstract class SelectableView : View, ISelectable {

        [Inject]
        public DeselectAllSignal DeselectAllSignal { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }
        
        protected override void Start() {
            base.Start();
            SelectionManager.RegisterSelectableObject(this);
            DeselectAllSignal.AddListener(Deselect);
        }

        public Projector selectableCircle;

        public void Deselect() {
            selectableCircle.enabled = false;
        }

        public void Select() {
            selectableCircle.enabled = true;
        }

        protected override void OnDestroy() {
            SelectionManager.UnregisterSelectableObject(this);
            DeselectAllSignal.RemoveListener(Deselect);
            base.OnDestroy();
        }


    }
}