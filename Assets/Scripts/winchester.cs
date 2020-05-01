using UnityEngine;

public class winchester : FireGun
{
    public AudioClip fireAndRechargeSound;
    public AudioClip blanckShotSound;
    public GameObject Bullet;
    public AudioSource audioSource;
    public float Bullet_Forward_Force;
    private float gunLastTrigger;

    void Start() {
        gunLastTrigger = Time.time - 1;
    }

    public override void fire(Vector3 direction) {
        float fireTime = Time.time;

        if (fireTime - gunLastTrigger > 1) {
            //TODO Envoyer un signal pour le UI et afficher lanimation du bouton feu en consequence

            audioSource.PlayOneShot(fireAndRechargeSound, 0.3f);

            gunLastTrigger = fireTime;
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
            Temporary_Bullet_Handler.tag = "Bullet";

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(direction * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 7 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 3.0f);
        } else {
            audioSource.PlayOneShot(blanckShotSound);
        }
    }
}
