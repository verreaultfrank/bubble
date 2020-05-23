using UnityEngine;
using UnityEngine.Networking;

public class sweepeer : FireGun {
    public AudioClip fireAndRechargeSound;
    public AudioClip blanckShotSound;
    public GameObject Bullet;
    private AudioSource audioSource;
    public float Bullet_Forward_Force;
    private float gunLastTrigger;
    private GameObject world;
    private bool onBlowingStreak;

    void Start() {
        audioSource = GameObject.FindObjectsOfType(typeof(AudioSource))[0] as AudioSource;
        world = GameObject.FindGameObjectWithTag("World");

        gunLastTrigger = Time.time - 1;
    }

    public override void CmdFire() {
        float fireTime = Time.time;

        if (fireTime - gunLastTrigger > 1 || onBlowingStreak) {
            //TODO Envoyer un signal pour le UI et afficher lanimation du bouton feu en consequence
            onBlowingStreak = true;
            audioSource.PlayOneShot(fireAndRechargeSound, 0.3f);

            gunLastTrigger = fireTime;
            
        } else {
            onBlowingStreak = false;
            audioSource.PlayOneShot(blanckShotSound);
        }
    }
}
