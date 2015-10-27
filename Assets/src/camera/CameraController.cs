using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public float height = 40f;
    public float angle = 40f;
    public float speed = 0.3f;
    public bool moveByMouse = true;

    private void Start() {
        transform.localRotation = Quaternion.Euler(angle, transform.localRotation.y, transform.localRotation.z);
        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
    }

    private void Update() {
        //CameraWidthPosition();
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
    }

    private void Translate(Vector3 pos) {
        // if SHIFT is held, move at double speed
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) pos *= 2.5f;

        // apply
        Vector3 r = transform.eulerAngles;
        r.x = 0;
        transform.position += Quaternion.Euler(r)*pos;
    }

    private void CameraWidthPosition() {
        //if (MS.NowMouseState == MouseState.MouseStats.Default)

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