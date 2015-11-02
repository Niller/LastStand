using System;
using UnityEngine;
using System.Collections;
using Assets.src.battle;
using Assets.src.views;
using strange.extensions.mediation.impl;

public class CameraController : View {
    public float height = 40f;
    public float angle = 40f;
    public float speed = 0.3f;
    public bool moveByMouse = true;
    public Transform target;

    [Inject]
    public ISelectionManager SelectionManager { get; set; }

    protected override void Start() {
        base.Start();
        transform.localRotation = Quaternion.Euler(angle, transform.localRotation.y, transform.localRotation.z);
        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
        SelectionManager.SelectionChanged += SelectionChanged;
    }

    private void SelectionChanged() {
        var selectedObjects = SelectionManager.GetSelectedObjects();
        if (selectedObjects.Count == 1) {
            var heroView = selectedObjects[0].GetView() as HeroView;
            if (heroView != null) {
                target = heroView.transform;
                return;
            }
        }
        target = null;
    }

    private void Update() {
        if (target == null) {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                Translate(Vector3.left*Time.deltaTime*speed);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                Translate(Vector3.right*Time.deltaTime*speed);
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                Translate(Vector3.forward*Time.deltaTime*speed);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                Translate(Vector3.back*Time.deltaTime*speed);
            }
            if (moveByMouse)
                CameraWidthPosition();
        } else {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - 50);
        }
    }

    private void Translate(Vector3 pos) {
        Vector3 r = transform.eulerAngles;
        r.x = 0;
        transform.position += Quaternion.Euler(r)*pos;
    }

    private void CameraWidthPosition() {
        if (20 > Input.mousePosition.x) {
            Translate(Vector3.left*Time.deltaTime*speed);
        }
        if ((Screen.width - 10) < Input.mousePosition.x) {
            Translate(Vector3.right*Time.deltaTime*speed);
        }
        if (20 > Input.mousePosition.y) {
            Translate(Vector3.back*Time.deltaTime*speed);
        }
        if ((Screen.height - 10) < Input.mousePosition.y) {
            Translate(Vector3.forward*Time.deltaTime*speed);
        }
    }
}