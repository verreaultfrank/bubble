using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bubble : NetworkBehaviour
{
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public GameObject grassBrand;

    private GrassMaker grassMaker;
    private Rigidbody rb;

    [SyncVar] public Vector3 currentPosition = Vector3.zero;
    [SyncVar] public Quaternion currentRotation = Quaternion.identity;

    [ClientRpc]
    private void RpcSyncPositionWithClients(Vector3 positionToSync) {
        currentPosition = positionToSync;
    }

    [ClientRpc]
    private void RpcSyncRotationWithClients(Quaternion rotationToSync) {
        currentRotation = rotationToSync;
    }

    void Start()
    {
        if (isServer) {
            grassMaker = new GrassMaker(grassBrand);

            rb = GetComponent<Rigidbody>();

            //Todo les enfants doivent se disperser en fonction de l'endroit de l'impact entre la munition et la boule parente
            if (this.tag == "Bubble") {
                rb.AddForce(new Vector3(150, 0, 150));
            } else if (this.tag == "Child1") {
                rb.AddForce(new Vector3(-150, 100, 150));
            } else {
                rb.AddForce(new Vector3(150, 100, -150));
            }
        }

    }

    void Start(Vector3 push)
    {
        if (isServer) {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(push);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update the posisitions for the different clients
        if (isServer) {
            RpcSyncPositionWithClients(this.transform.position);
            RpcSyncRotationWithClients(this.transform.rotation);
        }
    }

    private void LateUpdate() {
        if (!isServer) {
            this.transform.position = currentPosition;
            this.transform.rotation = currentRotation;
        }
    }

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

        if (this.transform.localScale.x >= 4)
        {
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

        }

        NetworkServer.Destroy(this.rb.gameObject);
    }

    private void OnCollisionWithWorld(Collision collision)
    {
        this.rb.AddRelativeForce(new Vector3(this.rb.velocity.x, 500, this.rb.velocity.z));

        //Pas une bonne idee selon les boys
        //grassMaker.makeGrass(collision.contacts[0].point, (int)Math.Round(this.transform.localScale.x),  1);
    }

    private int findEmptyLayer() {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<int> layerList = new List<int>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer != 0)
            {
                layerList.Add(i);
            }
        }

        layerList.Sort();

        return layerList[layerList.Count - 1] + 1;
    }

}
