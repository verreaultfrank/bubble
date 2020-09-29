using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float speed;

    private Vector3 moveDirKeyboard;
    private Vector3 moveDirJoyStick;
    
    private Rigidbody rb;

    protected Joystick joystick;
    protected JoyButton1 joyButton1;
    protected JoyButton2 joyButton2;


    public override void OnStartLocalPlayer() {
        (GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraController") as CameraController).player = this.transform.gameObject;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
        joystick = FindObjectOfType<Joystick>();
        joyButton1 = FindObjectOfType<JoyButton1>();
        joyButton2 = FindObjectOfType<JoyButton2>();
    }

    private void Update() {
        getInput();
        moveLocalPlayer();
    }

    private void getInput() {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        moveDirKeyboard = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        moveDirJoyStick = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical).normalized;
    }

    private void moveLocalPlayer() {
        if(isLocalPlayer)
            rb.MovePosition(rb.position + (transform.TransformDirection(moveDirKeyboard) + transform.TransformDirection(moveDirJoyStick)) * speed * Time.fixedDeltaTime);
    }

    private bool isButton1Pressed() {
        return joyButton1.Pressed;
    }
}
