using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Press_Space_To_Fire : NetworkBehaviour {
    //Drag in the Bullet Emitter from the Component Inspector.
    private FireGun bulletEmitter;

    private GameObject World;

    protected JoyButton1 joyButton1;


    // Use this for initialization
    void Start()
    {
        bulletEmitter = gameObject.GetComponent("winchester") as FireGun;

        World = GameObject.FindGameObjectWithTag("World");

        joyButton1 = FindObjectOfType<JoyButton1>();
    }

    // Update is called once per frame
    void Update()
    {   if (!isLocalPlayer)
            return;

        FireBulletEmitter();
    }

    void FireBulletEmitter() {
        float fireTime = Time.time;
        if ((Input.GetKeyDown("space") || (joyButton1 != null && joyButton1.Pressed)) && isLocalPlayer) {
            bulletEmitter.CmdFire();
        }
    }
}