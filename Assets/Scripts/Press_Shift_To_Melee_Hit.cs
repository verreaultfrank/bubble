using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Press_Shift_To_Melee_Hit : NetworkBehaviour {
    //Drag in the Bullet Emitter from the Component Inspector.
    private MeleeWeapon sword;

    protected JoyButton2 joyButton2;


    // Use this for initialization
    void Start() {
        sword = gameObject.GetComponent("Sword") as MeleeWeapon;

        joyButton2 = FindObjectOfType<JoyButton2>();
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer)
            return;

        HitWithHandWeapon();
    }

    void HitWithHandWeapon() {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || (joyButton2 != null && joyButton2.Pressed)) && isLocalPlayer) {
            sword.CmdHit();
        }
    }
}