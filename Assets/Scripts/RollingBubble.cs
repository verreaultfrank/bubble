using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RollingBubble : Bubble {
    [Server]
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "PlayerSword")
            OnCollisionWithSword(collider);
    }

    private void OnCollisionWithSword(Collider collider) {
        audioSource.PlayOneShot(explosionSound);
        NetworkServer.Destroy(this.rb.gameObject);
    }

}
