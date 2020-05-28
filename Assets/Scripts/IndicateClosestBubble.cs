using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IndicateClosestBubble : NetworkBehaviour
{
    private Rigidbody player;
    private GameObject world;
    public float indicatorHeight;
    private GameObject indicator;


    // Start is called before the first frame update
    void Start() {
        if (!isLocalPlayer)
            return;

        world = GameObject.FindGameObjectWithTag("World");
        player = this.gameObject.GetComponent("Rigidbody") as Rigidbody;
        indicator = GameObject.FindGameObjectWithTag("ClosestBubbleIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float maxDistance = float.MaxValue;
        BouncingBubble[] lstBubble = FindObjectsOfType<BouncingBubble>();
        BouncingBubble theChosenOne = null;

        foreach (BouncingBubble bubble in lstBubble) {
            float distToBubble = (player.position - bubble.transform.position).magnitude;
            if (maxDistance > distToBubble) {
                maxDistance = distToBubble;
                theChosenOne = bubble;
            }
        }

        if (theChosenOne != null) {
            float distToChosenOne = (player.position - theChosenOne.transform.position).magnitude;

            if (distToChosenOne > 20) {
                indicator.SetActive(true);
                var worldToPlayerUnit = (player.transform.position - world.transform.position).normalized;
                var worldTotheChosenOneUnit = (theChosenOne.transform.position - world.transform.position).normalized;

                //On bouge la camera vers le joueur comme si les deux etait a la meme distance du centre du monde. On le bouge de la difference entre la distance entre les deux et le maximumDist
                Vector3 slerpChosenOneToPlayer = Vector3.Slerp(worldToPlayerUnit * indicatorHeight, worldTotheChosenOneUnit * indicatorHeight, 0.08f);

                //Param inverse pour que ca fonctionne idk why
                indicator.transform.rotation = Quaternion.LookRotation(worldToPlayerUnit, slerpChosenOneToPlayer);
                indicator.transform.position = slerpChosenOneToPlayer;
            } else {
                indicator.transform.position = player.position;
                indicator.SetActive(false);
            }
        }
    }
}
