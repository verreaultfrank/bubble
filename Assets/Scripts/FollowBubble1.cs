using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBubble1 : MonoBehaviour {

    private Light spotlight;
    // Use this for initialization
    void Start () {
        spotlight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update () {
        /*Je veux que le spothlight reste toujours à la même distance par rapport à la surface ou le centre du world*/
        transform.position = new Vector3(spotlight.transform.position.x, spotlight.transform.position.y, spotlight.transform.position.z);
    }
}
