using UnityEngine;
using UnityEngine.Networking;

public class Sword : MeleeWeapon {
    public AudioClip sliceSound;
    private AudioSource audioSource;
    private float swordLastHitTimer;
    public GameObject sword;


    void Start() {
        audioSource = GameObject.FindObjectsOfType(typeof(AudioSource))[0] as AudioSource;
        swordLastHitTimer = Time.time - 1;
    }

    public override void CmdHit() {
        float fireTime = Time.time;

        if (fireTime - swordLastHitTimer > 1) {
            //TODO Envoyer un signal pour le UI et afficher lanimation du bouton feu en consequence
            audioSource.PlayOneShot(sliceSound, 1f);

            swordLastHitTimer = fireTime;
            Animation testAnim = sword.GetComponent<Animation>();
            sword.GetComponent<Animation>().Play("swordSlash");
        } 
    }
}
