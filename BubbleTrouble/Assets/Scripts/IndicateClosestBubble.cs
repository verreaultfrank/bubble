using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateClosestBubble : MonoBehaviour
{
    public Rigidbody player;
    public GameObject world;
    public float indicatorHeight;
    private Transform arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float maxDistance = float.MaxValue;
        Bubble[] lstBubble = FindObjectsOfType<Bubble>();
        Bubble theChosenOne = null;

        foreach (Bubble bubble in lstBubble)
        {
            float distToBubble = (player.position - bubble.transform.position).magnitude;
            if (maxDistance > distToBubble)
            {
                maxDistance = distToBubble;
                theChosenOne = bubble;
            }
        }

        if (theChosenOne != null)
        {
            float distToChosenOne = (player.position - theChosenOne.transform.position).magnitude;

            if (distToChosenOne > 20)
            {
                var worldToPlayerUnit = (player.transform.position - world.transform.position).normalized;
                var worldTotheChosenOneUnit = (theChosenOne.transform.position - world.transform.position).normalized;

                //On bouge la camera vers le joueur comme si les deux etait a la meme distance du centre du monde. On le bouge de la difference entre la distance entre les deux et le maximumDist
                Vector3 slerpChosenOneToPlayer = Vector3.Slerp(worldToPlayerUnit * indicatorHeight, worldTotheChosenOneUnit * indicatorHeight, 0.08f);

                //Param inverse pour que ca fonctionne idk why
                transform.rotation = Quaternion.LookRotation(worldToPlayerUnit, slerpChosenOneToPlayer);
                transform.position = slerpChosenOneToPlayer;
            }
            else {
                transform.position = player.position;
            }
        }
    }
}
