using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAttraction : MonoBehaviour
{

    public Gravity attractor;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    void Update()
    {
        attractor.Attract(rb);
    }

}
