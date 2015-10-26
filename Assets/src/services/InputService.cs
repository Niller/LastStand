using Assets.src.contexts;
using Assets.src.signals;
using UnityEngine;

namespace Assets.src.contexts {
    public class InputService : IInputService {

        [Inject]
        public OnClickSignal OnClick { get; set; }

        [Inject]
        public OnDragStartSignal OnDragStart { get; set; }

        [Inject]
        public OnDragSignal OnDrag { get; set; }

        [Inject]
        public OnDragEndSignal OnDragEnd { get; set; }

        protected bool isInitialized;

        protected bool isMouseKeyDown;
        
        protected bool isDrag;

        protected const float MinDragDelta = 10f;

        protected Vector3 startDragScreenPos;
        
        [PostConstruct]
        public void PostConstructor() {
            isInitialized = true;
        }

        public void OnUpdate() {
            if (!isInitialized)
                return;

            if (isDrag) {
                OnDrag.Dispatch(GetMousePosition());
            }

            if (isMouseKeyDown && !isDrag) {
                if (Vector2.Distance(startDragScreenPos, GetMousePosition()) >= MinDragDelta) {
                    OnDragStart.Dispatch(startDragScreenPos);
                    isDrag = true;
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                isMouseKeyDown = true;
                startDragScreenPos = GetMousePosition();
            }
            
            if (Input.GetMouseButtonUp(0)) {
                isMouseKeyDown = false;
                if (isDrag) {
                    OnDragEnd.Dispatch(new[] { startDragScreenPos, GetMousePosition()});
                    isDrag = false;
                } else {
                    OnClick.Dispatch(GetMousePosition());
                }
            }
        }

        protected Vector3 GetMousePosition() {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            return mousePos;
        }
    }
}