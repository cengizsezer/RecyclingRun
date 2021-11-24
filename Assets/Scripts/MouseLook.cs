using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity;
    public float xRotation = 0f;
    public Transform BodyTransform;
    GameController gameController;


    private void Start()
    {
        gameController = GameController.request();
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(gameController.IsGameStarted)
        {
            MouseInput();
        }
        
    }

    void MouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        this.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        BodyTransform.Rotate(Vector3.up * mouseX);

    }
}
