using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookTPS : MonoBehaviour
{
    public float mouseSensitivity = 400f;
    public float zoomModifier = 1f;

    public float zoom = 0f;

    public Transform playerBody;
    public Transform cameraPosition;

    Vector3 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        // Locking the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        zoom -= Input.GetAxis("Mouse ScrollWheel") * mouseSensitivity * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, 1f, 8f);

        //
        playerBody.Rotate(Vector3.up* mouseX);

        cameraPos.x = 0f;
        cameraPos.y = 8f + zoom;
        cameraPos.z = -8f - zoom;
        transform.localPosition = cameraPos;
    }    
}
