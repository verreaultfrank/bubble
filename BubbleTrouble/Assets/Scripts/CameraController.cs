using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public GameObject world;

    public float cameraHeight;

    float maxDistCamPlayer = 22;

    // Use this for initialization
    void Start () {

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > maxDistCamPlayer)
        {
            var worldToPlayerUnit = (player.transform.position - world.transform.position).normalized;
            var worldToCameraUnit = (transform.position - world.transform.position).normalized;

            //On bouge la camera vers le joueur comme si les deux etait a la meme distance du centre du monde. On le bouge de la difference entre la distance entre les deux et le maximumDist
            var slerpCameraToPlayer = Vector3.Slerp(worldToCameraUnit * cameraHeight, worldToPlayerUnit * cameraHeight, (distance - maxDistCamPlayer) / distance);

            //transform.position = slerpCameraToPlayer;
            Quaternion rotation = new Quaternion();
            rotation.SetFromToRotation(transform.position, slerpCameraToPlayer);

            Vector3 newCamPos = slerpCameraToPlayer;
            Quaternion newCamRotation = rotation * transform.rotation;

            transform.SetPositionAndRotation(newCamPos, newCamRotation);
        }
    }
}
