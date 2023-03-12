using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 400f;

    public Transform playerBody;

    float xRotation = 0f;

    public static bool allowHeadMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Locking the cursor
        Cursor.lockState = CursorLockMode.Locked;
        allowHeadMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowHeadMovement) return;
        // Getting mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation + clamping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //
        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        
    }
}
