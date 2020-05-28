using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour
{
    public GameObject bouncingBubble;

    private void Start() {
        Spawn();
    }

    private void Spawn() {
        if (isServer) {
            GameObject child1 = GameObject.Instantiate(bouncingBubble, new Vector3(10, 39, 3.5f), new Quaternion());
            NetworkServer.Spawn(child1);
        }
    }
}
