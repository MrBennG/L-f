﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;

    public int zoomSpeed;
    public float panDetect = 30f;
    public bool InvertZoom = false;
    public float minHeight = 40f;
    public float maxHeight = 300f;
    
    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }
    }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;
        

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if (Input.GetKey(KeyCode.A) || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed;
        }

        if (Input.GetKey(KeyCode.W) || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed;
        }
        else if (Input.GetKey(KeyCode.S) || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed;
        }

        if (InvertZoom == false)
        {
            moveY -= Input.GetAxis("Mouse ScrollWheel") * (zoomSpeed);
        }
        else
        {
            moveY += Input.GetAxis("Mouse ScrollWheel") * (zoomSpeed);
        }

        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);
       
        Vector3 newPos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = newPos;
    }

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if (Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;
        }

        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
        }
    }
}
