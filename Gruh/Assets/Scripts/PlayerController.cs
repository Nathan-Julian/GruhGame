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
    public float airAccel;
    public float groundSpeedCap;

    float GroundCastLength = 2;

    //input setup
    string leftKey = "a";
    string rightKey = "d";
    string forwardKey = "w";
    string backKey = "s";




    void Start()
    {
        
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
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            PlayerRB.drag = groundDrag;
            //input check
            if (Input.GetKey(leftKey))
            {
                PlayerRB.AddRelativeForce(-groundAccel * Time.deltaTime * 100, 0, 0);
            }
            if(Input.GetKey(rightKey))
            {
                PlayerRB.AddRelativeForce(groundAccel * Time.deltaTime * 100, 0, 0);
            }
            if (Input.GetKey(forwardKey))
            {
                //PlayerRB.AddRelativeForce((groundAccel * -(float)Math.Pow((Vector3.Dot(PlayerRB.velocity, transform.forward) * transform.forward.normalized.z + groundSpeedCap), 3D)) * Time.deltaTime, 0, 0);
                //PlayerRB.AddRelativeForce(groundAccel * Vector3.Dot(PlayerRB.velocity, transform.forward) * transform.forward.normalized.z * Time.deltaTime, 0, 0);
                PlayerRB.AddRelativeForce(0, 0, groundAccel * Time.deltaTime * 100);
            }
            if(Input.GetKey(backKey))
            {
                PlayerRB.AddRelativeForce(0, 0, -groundAccel * Time.deltaTime * 100);
            }
        }
        else
        {
            //didn't hit - in air
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.down) * 1000, Color.white);
            PlayerRB.drag = 0.01F;
            PlayerRB.AddForce(0, -gravity * Time.deltaTime, 0); //add gravity force
        }
    }
}

