using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera PlayerCamera;
    public int gravity;
    public Rigidbody PlayerRB;
    public float groundDrag;
    public float airDrag;
    public float groundAccel;
    public float sprintAccel;
    public float airAccel;
    public float groundSpeedCap;
    public float jumpForce;
    public float mouseSensitivity;
    

    float GroundCastLength = 2;

    //input setup
    string jumpKey = "space";
    string crouchKey = "shift";

    float xRotation = 0f;
    float horizontalMovement = 0f;
    float verticalMovement = 0f;
    bool onGround;
    bool isSprinting = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Movement(bool onGround, bool sprinting, bool jump, float groundAccel, float sprintAccel, float airAccel, float jumpForce)
    {
        if(onGround)
        {
            if()
            PlayerRB.ApplyRelativeForce(horizontalMovement, 0, verticalMovement);
        }
    }

    void Update()
    {
        //keyboard controls
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        //test

        //Ground Check Raycast setup
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        
        //Ground Check Raycast
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.down), out hit, GroundCastLength, layerMask))
        {
            //did hit - on ground
            Debug.Log("touching Ground");
            onGround = true;

            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            PlayerRB.drag = groundDrag;
            
        }
        else
        {
            //didn't hit - in air
            Debug.Log("in air");
            onGround = false;
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.down) * 1000, Color.white);
            PlayerRB.drag = 0.01F;
            PlayerRB.AddForce(0, -gravity * Time.deltaTime, 0); //add gravity force
        }


        //mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
    }
}

