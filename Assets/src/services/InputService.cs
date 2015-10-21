using Assets.src.contexts;
using UnityEngine;

namespace Assets.src.contexts {
    public class InputService : IInputService {

        [Inject]
        public OnClickSignal OnClick { get; set; }

        protected bool isInitialized;
        
        [PostConstruct]
        public void PostConstructor() {
            isInitialized = true;
        }

        public void OnUpdate() {
            if (!isInitialized)
                return;
            if (Input.GetMouseButtonUp(0)) {
				var mousePos = Input.mousePosition;
				mousePos.z = Camera.main.nearClipPlane;
                OnClick.Dispatch(mousePos);
            }
        }
    }
}