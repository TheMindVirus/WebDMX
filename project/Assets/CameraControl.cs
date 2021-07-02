using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float speed = 0.1f;
    float sensitivity = 1.5f;
    float floor = 0.5f;
    Vector3 facing = new Vector3(0, 0, 0);
    Vector3 direction = new Vector3(0, 0, 0);
    Vector3 newpos = new Vector3(0, 0, 0);

    void Start()
    {
        //Use with Web Pointer Lock API
    }

    void Update()
    {
        facing = new Vector3(transform.eulerAngles.x + (Input.GetAxis("Mouse Y") * sensitivity * -1.0f),
                                transform.eulerAngles.y + (Input.GetAxis("Mouse X") * sensitivity), 0);
        transform.eulerAngles = facing;

        if (Input.GetKey(KeyCode.W)) { direction += new Vector3( 0,  0,  1); }
        if (Input.GetKey(KeyCode.S)) { direction += new Vector3( 0,  0, -1); }
        if (Input.GetKey(KeyCode.A)) { direction += new Vector3(-1,  0,  0); }
        if (Input.GetKey(KeyCode.D)) { direction += new Vector3( 1,  0,  0); }
        direction = direction * speed;
        transform.Translate(direction);
        newpos = transform.position;
        if (newpos.y < floor) { newpos.y = floor; }
        transform.position = newpos;
    }
}