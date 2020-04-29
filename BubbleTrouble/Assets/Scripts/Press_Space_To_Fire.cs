using UnityEngine;
using System.Collections;

public class Press_Space_To_Fire : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public FireGun bullet_Emitter;

    public GameObject World;

    protected JoyButton1 joyButton1;


    // Use this for initialization
    void Start()
    {
        joyButton1 = FindObjectOfType<JoyButton1>();
    }

    // Update is called once per frame
    void Update()
    {
        float fireTime = Time.time;
        if (Input.GetKeyDown("space") || (joyButton1 != null && joyButton1.Pressed)){
            //Unit vector planet vector
            Vector3 direction = transform.position - World.transform.position;

            bullet_Emitter.fire(direction);
        }
     
    }
}