using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //[SerializeField]
    //GameObject PlayerObject;

    [SerializeField]
    bool lockXMovement;

    //float xLimits = 20;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Use this for initialization
    void Start () {
		// cursor locking functions 
		Cursor.lockState = CursorLockMode.Locked; 
		Cursor.visible = false; 
	}

    void Update()
    {
        if (lockXMovement)
        {
            yaw = transform.eulerAngles.y;
        }
        else
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            
            // Attempt to lock cameraX to range. but seems more troublesome than it is worth
            /*
            if (yaw > xLimits + PlayerObject.transform.eulerAngles.y)
            {
                yaw = xLimits + PlayerObject.transform.eulerAngles.y;
            }
            else if (yaw < -xLimits - PlayerObject.transform.eulerAngles.y)
            {
                yaw = -xLimits - PlayerObject.transform.eulerAngles.y;
            }
            */
        }
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
