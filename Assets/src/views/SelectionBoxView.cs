using Assets.src.battle;
using Assets.src.signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.src.views {
    public class SelectionBoxView : View {

        [Inject]
        public OnDragStartSignal OnDragStart { get; set; }

        [Inject]
        public OnDragSignal OnDrag { get; set; }

        [Inject]
        public OnDragEndSignal OnDragEnd { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        private bool isShow;

        private Vector3 startPosition;

        private Vector3 endPosition;

        protected override void Start() {
            base.Start();
            OnDrag.AddListener(OnDragHandler);
            OnDragStart.AddListener(OnDragStartHandler);
            OnDragEnd.AddListener(OnDragEndListner);
        }

        private void OnDragEndListner(Vector3[] obj) {
            isShow = false;
        }

        private void OnDragStartHandler(Vector3 position) {
            isShow = true;
            startPosition = position;
            endPosition = position;
            
        }

        private void OnDragHandler(Vector3 position) {
            endPosition = position;
            
        }

        private Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2) {
            // Move origin from bottom left to top left
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;
            // Calculate corners
            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
            // Create Rect
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        Texture2D _whiteTexture;
        public Texture2D WhiteTexture {
            get {
                if (_whiteTexture == null) {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }

                return _whiteTexture;
            }
        }

        public void DrawScreenRect(Rect rect, Color color) {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
            GUI.color = Color.white;
        }

        public void DrawScreenRectBorder(Rect rect, float thickness, Color color) {
            // Top
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            // Left
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            // Right
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            // Bottom
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        }

        private void OnGUI() {
            if (isShow && !GameManager.IsControlBlocked()) {
                var rect = GetScreenRect(startPosition, endPosition);
                DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
                DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
            }
        }
    }
}