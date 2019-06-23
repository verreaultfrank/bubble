using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public GameObject world;

    public float cameraHeight;

    Vector3 newCamPos;

    Quaternion newCamRotation;

    float maxDistCamPlayer = 22;

    // Use this for initialization
    void Start () {

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > maxDistCamPlayer)
        {
            var worldToPlayer = player.transform.position - world.transform.position;
            var worldToPlayerUnit = worldToPlayer / worldToPlayer.magnitude;

            var worldToCamera = transform.position - world.transform.position;
            var worldToCameraUnit = worldToCamera / worldToCamera.magnitude;

            //On bouge la camera vers le joueur comme si les deux etait a la meme distance du centre du monde. On le bouge de la difference entre la distance entre les deux et le maximumDist
            var slerpCameraToPlayer = Vector3.Slerp(worldToCameraUnit * cameraHeight, worldToPlayerUnit * cameraHeight, (distance - maxDistCamPlayer) / distance);

            //transform.position = slerpCameraToPlayer;
            Quaternion rotation = new Quaternion();
            rotation.SetFromToRotation(transform.position, slerpCameraToPlayer);

            newCamPos = slerpCameraToPlayer;
            newCamRotation = rotation * transform.rotation;
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > maxDistCamPlayer)
        {
            transform.SetPositionAndRotation(newCamPos, newCamRotation);
        }
    }
}
