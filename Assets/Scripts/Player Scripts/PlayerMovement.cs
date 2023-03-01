using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float runningModifier = 2f;
    public float gravity = -10f;
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool cursorLocked = true;

    public GameObject cam1;
    public GameObject cam2;

    public bool isFPS = true;

    private void Start()
    {
        cam1.SetActive(isFPS);
        cam2.SetActive(!isFPS);
    }


    // Update is called once per frame
    void Update()
    {
        // gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // x z movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);

        // run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(runningModifier * speed * Time.deltaTime * move);
        } else
        {
            controller.Move(speed * Time.deltaTime * move);
        }

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        

        
        // switch camera mode between fps and tps
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFPS = !isFPS;
            if (isFPS)
            {
                cam1.SetActive(false);
                cam2.SetActive(true);
            }
            else
            {
                cam1.SetActive(true);
                cam2.SetActive(false);
            }
        }
        // Left alt allows to toggle mouse display
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (cursorLocked == true)
            {
                cursorLocked = false;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                cursorLocked = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }
    }
}
