using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    float speed = 0.1f;
    float sensitivity = 2.0f;
    float floor = 0.5f;
    float deadzone = 0.1f;
    Vector3 facing = new Vector3(0, 0, 0);
    Vector3 direction = new Vector3(0, 0, 0);
    Vector3 newpos = new Vector3(0, 0, 0);

    void Start()
    {
        //Use with Web Pointer Lock API
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) { return; }
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();
        leftStick = new Vector2((Mathf.Abs(leftStick.x) > deadzone) ? leftStick.x * sensitivity : 0.0f, (Mathf.Abs(leftStick.y) > deadzone) ? leftStick.y * sensitivity : 0.0f);
        rightStick = new Vector2((Mathf.Abs(rightStick.x) > deadzone) ? rightStick.x * sensitivity : 0.0f, (Mathf.Abs(rightStick.y) > deadzone) ? rightStick.y * sensitivity : 0.0f);

        //facing = new Vector3(transform.eulerAngles.x + (Input.GetAxis("Mouse Y") * sensitivity * -1.0f),
        //                transform.eulerAngles.y + (Input.GetAxis("Mouse X") * sensitivity), 0);
        //facing = new Vector3(transform.eulerAngles.x + (Input.GetAxis("LeftStickVertical") * sensitivity * -1.0f),
        //                        transform.eulerAngles.y + (Input.GetAxis("LeftStickHorizontal") * sensitivity), 0);
        facing = new Vector3(transform.eulerAngles.x + (rightStick.y * -1.0f),
                                transform.eulerAngles.y + (rightStick.x), 0);
        transform.eulerAngles = facing;

        //if (Input.GetKey(KeyCode.W)) { direction += new Vector3( 0,  0,  1); }
        //if (Input.GetKey(KeyCode.S)) { direction += new Vector3( 0,  0, -1); }
        //if (Input.GetKey(KeyCode.A)) { direction += new Vector3(-1,  0,  0); }
        //if (Input.GetKey(KeyCode.D)) { direction += new Vector3( 1,  0,  0); }
        direction = new Vector3(leftStick.x, 0, leftStick.y * -1.0f);
        direction = direction * speed;
        transform.Translate(direction);
        newpos = transform.position;
        if (newpos.y < floor) { newpos.y = floor; }
        transform.position = newpos;
    }
}