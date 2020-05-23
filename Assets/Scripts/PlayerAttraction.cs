using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAttraction : MonoBehaviour
{

    private Gravity attractor;
    private Rigidbody rb;

    void Start()
    {
        attractor = GameObject.FindGameObjectWithTag("World").GetComponent("Gravity") as Gravity;

        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    void Update()
    {
        attractor.Attract(rb);
    }

}
