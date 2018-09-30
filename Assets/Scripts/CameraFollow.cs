using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 cameraFollowPosition;


    public void Setup(Vector3 cameraFollowPosition)
    {
        this.cameraFollowPosition = cameraFollowPosition;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
}
