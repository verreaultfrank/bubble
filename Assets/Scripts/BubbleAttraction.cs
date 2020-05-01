using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BubbleAttraction : MonoBehaviour
{

    public Gravity attractor;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }

    void Update()
    {
        attractor.Attract(rb);
    }

}
