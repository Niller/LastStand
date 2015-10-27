using Assets.src.views;
using UnityEngine;

namespace Assets.src.services {
    public class GameResourcesService : IGameResourcesService {

        protected Texture2D targetCursor;

        protected AreaSelectorView areaSelector;

        public void Initialize() {
            targetCursor = Resources.Load<Texture2D>("textures/crosshair");
            areaSelector = Object.FindObjectOfType<AreaSelectorView>();
        }

        public void OnUpdate() {
            
        }

        public Texture2D GetTargetCursor() {
            return targetCursor;
        }

        public AreaSelectorView GetAreaSelector() {
            return areaSelector;
        }  
    }
}