using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook2 : MonoBehaviour {
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private Transform playerBody;

    private float verticalRotation = 0.0f;

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;

        verticalRotation = 0.0f;
    }

    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation += mouseY;

        if (verticalRotation > 90.0f) {
            // looking up more than 90 degrees
            verticalRotation = 90.0f;
            fixVerticalRotation(270.0f);
        } else if (verticalRotation < -90.0f) {
            verticalRotation = -90.0f;
            fixVerticalRotation(90.0f);
        } else {
            transform.Rotate(Vector3.left * mouseY);
        }

        playerBody.transform.Rotate(Vector3.up * mouseX);
    }

    private void fixVerticalRotation(float verticalRotation) {
        Vector3 angles = transform.eulerAngles;
        angles.x = verticalRotation;
        transform.eulerAngles = angles;
    }
}
