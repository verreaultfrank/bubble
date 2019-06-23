using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Vector3 moveDirKeyboard;
    private Vector3 moveDirJoyStick;
    private Rigidbody rb;

    protected Joystick joystick;
    protected JoyButton1 joyButton1;
    protected JoyButton2 joyButton2;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();
        joyButton1 = FindObjectOfType<JoyButton1>();
        joyButton2 = FindObjectOfType<JoyButton2>();
   
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirKeyboard = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        moveDirJoyStick = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirKeyboard) * speed * Time.fixedDeltaTime + transform.TransformDirection(moveDirJoyStick) * speed * Time.fixedDeltaTime);
    }

    private bool isButton1Pressed() {
        return joyButton1.Pressed;
    }
}
