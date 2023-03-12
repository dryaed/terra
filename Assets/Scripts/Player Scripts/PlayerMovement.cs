using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 14f;
    public float runningModifier = 1.5f;
    public float gravity = -40f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    Vector3 _velocity;
    bool _isGrounded;
    bool _cursorLocked = true;

    public GameObject cam1;
    public GameObject cam2;

    public bool isFPS = true;

    private void Start()
    {
        cam1.SetActive(isFPS);
        cam2.SetActive(!isFPS);
        Physics.IgnoreLayerCollision(0, 7); // no collision between Default[0] and Item[7] layers
    }


    // Update is called once per frame
    void Update()
    {
        // gravity
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        // x z movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);

        // run
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            controller.Move(runningModifier * speed * Time.deltaTime * move);
        } else
        {
            controller.Move(speed * Time.deltaTime * move);
        }

        // jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);

        

        
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
            if (_cursorLocked == true)
            {
                _cursorLocked = false;
                MouseLook.allowHeadMovement = false;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                _cursorLocked = true;
                MouseLook.allowHeadMovement = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }
        
        
    }
}
