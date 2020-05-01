using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip explosionSound;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Todo les enfants doivent se disperser en fonction de l'endroit de l'impact entre la munition et la boule parente
        if (this.tag == "Bubble")
        {
            rb.AddForce(new Vector3(150, 0, 150));
        }
        else if (this.tag == "Child1")
        {
            rb.AddForce(new Vector3(-150, 100, 150));
        }
        else {
            rb.AddForce(new Vector3(150, 100, -150));
        }
    }

    void Start(Vector3 push)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(push);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            OnCollisionWithBullet(collision);


        if (collision.gameObject.tag == "World")
            OnCollisionWithWorld();
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

            GameObject child2 = GameObject.Instantiate(this.gameObject);
            child2.layer = findEmptyLayer();
            Light child2Light = child2.GetComponentInChildren<Light>();
            child2Light.range = child2Light.range / 2;
            child2Light.cullingMask = child2Light.cullingMask | (1 << child2.layer);
            child2.tag = "Child2";
            child2.transform.localScale = newScale;
        }

        Destroy(this.rb.gameObject);
    }

    private void OnCollisionWithWorld()
    {
        this.rb.AddRelativeForce(new Vector3(this.rb.velocity.x, 500, this.rb.velocity.z));
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
