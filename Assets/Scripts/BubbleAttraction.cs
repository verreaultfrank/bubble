using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BubbleAttraction : MonoBehaviour
{

    private Gravity attractor;
    private Rigidbody rb;

    void Start()
    {
        attractor = GameObject.FindObjectsOfType(typeof(Gravity))[0] as Gravity;

        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }

    void Update()
    {
        attractor.Attract(rb);
    }

}
