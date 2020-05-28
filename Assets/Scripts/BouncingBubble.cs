using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BouncingBubble : Bubble
{
    public GameObject RollingBubblePrefab;

    //private GrassMaker grassMaker;

    [Server]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            OnCollisionWithBullet(collision);


        if (collision.gameObject.tag == "World")
            OnCollisionWithWorld(collision);
    }

    private void GrowGrassUnder() {
        GameObject child2 = GameObject.Instantiate(this.gameObject);
    }

    private void OnCollisionWithBullet(Collision collision) {
        audioSource.PlayOneShot(explosionSound);
        Destroy(collision.gameObject);

        if (this.transform.localScale.x >= 4) {
            Vector3 newScale = new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, this.transform.localScale.z / 2);

            GameObject child1 = GameObject.Instantiate(this.gameObject);
            child1.layer = findEmptyLayer();
            Light child1Light = child1.GetComponentInChildren<Light>();
            child1Light.range = child1Light.range / 2;
            child1Light.cullingMask = child1Light.cullingMask | (1 << child1.layer);
            child1.tag = "Child1";
            child1.transform.localScale = newScale;
            NetworkServer.Spawn(child1);

            GameObject child2 = GameObject.Instantiate(this.gameObject);
            child2.layer = findEmptyLayer();
            Light child2Light = child2.GetComponentInChildren<Light>();
            child2Light.range = child2Light.range / 2;
            child2Light.cullingMask = child2Light.cullingMask | (1 << child2.layer);
            child2.tag = "Child2";
            child2.transform.localScale = newScale;
            NetworkServer.Spawn(child2);
        } else {
            Vector3 newScale = new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, this.transform.localScale.z / 2);

            GameObject child1 = GameObject.Instantiate(RollingBubblePrefab, transform.position, transform.rotation);
            child1.tag = "Child1";
            child1.transform.localScale = newScale;
            NetworkServer.Spawn(child1);

            GameObject child2 = GameObject.Instantiate(RollingBubblePrefab, transform.position, transform.rotation);
            child2.tag = "Child2";
            child2.transform.localScale = newScale;
            NetworkServer.Spawn(child2);
        }

        NetworkServer.Destroy(this.rb.gameObject);
    }

    private void OnCollisionWithWorld(Collision collision)
    {
        this.rb.AddRelativeForce(new Vector3(this.rb.velocity.x, 500, this.rb.velocity.z));

        //Pas une bonne idee selon les boys
        //grassMaker.makeGrass(collision.contacts[0].point, (int)Math.Round(this.transform.localScale.x),  1);
    }
}
