using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private GameObject world;

    readonly float r1 = 50.5f;
    readonly float r2 = 22.0f;

    // Use this for initialization
    void Start () {
        world = GameObject.FindGameObjectWithTag("World");
    }

    void Update()
    {
        if (player != null) {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist > r2) {
                float x1, x2, y1, y2, z1, z2, d, h, pX, pY, pZ, pD, cX, cY, cZ;
                Vector3 playerPos, worldPos, oldCameraPos, newCameraPos, circleCenter, pmc, qmc;

                // Camera and player spheres intersection
                playerPos = player.transform.position;
                worldPos = world.transform.position;
                x1 = worldPos.x;
                y1 = worldPos.y;
                z1 = worldPos.z;
                x2 = playerPos.x;
                y2 = playerPos.y;
                z2 = playerPos.z;
                d = (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));
                h = (float)Math.Sqrt(4 * r1 * r1 * d * d - (r1 * r1 + d * d - r2 * r2) * (r1 * r1 + d * d - r2 * r2)) / (2 * d);
                pX = 2 * (x2 - x1);
                pY = 2 * (y2 - y1);
                pZ = 2 * (z2 - z1);
                pD = x1 * x1 - x2 * x2 + y1 * y1 - y2 * y2 + z1 * z1 - z2 * z2 - r1 * r1 + r2 * r2;
                cX = x1 - (x2 - x1) * (pX * x1 + pY * y1 + pZ * z1 + pD) / (pX * (x2 - x1) + pY * (y2 - y1) + pZ * (z2 - z1));
                cY = y1 - (y2 - y1) * (pX * x1 + pY * y1 + pZ * z1 + pD) / (pX * (x2 - x1) + pY * (y2 - y1) + pZ * (z2 - z1));
                cZ = z1 - (z2 - z1) * (pX * x1 + pY * y1 + pZ * z1 + pD) / (pX * (x2 - x1) + pY * (y2 - y1) + pZ * (z2 - z1));

                // Closest point between old camera and intersection circle
                oldCameraPos = transform.position;
                circleCenter = new Vector3(cX, cY, cZ);
                pmc = oldCameraPos - circleCenter;
                qmc = pmc - Vector3.Dot(circleCenter.normalized, pmc) * circleCenter.normalized;

                // Normal case
                if (qmc.magnitude > 0) {
                    newCameraPos = circleCenter + (h / qmc.magnitude) * qmc;
                }
                // All circle points are equidistant from old camera (shouldn't happen)
                else {
                    newCameraPos = (player.transform.position - world.transform.position).normalized * r1;
                }

                transform.position = newCameraPos;

                transform.LookAt(world.transform); // TODO: Fix this shit

                /*var worldToPlayerUnit = (player.transform.position - world.transform.position).normalized;
                var worldToCameraUnit = (transform.position - world.transform.position).normalized;

                //On bouge la camera vers le joueur comme si les deux etait a la meme distance du centre du monde. On le bouge de la difference entre la distance entre les deux et le maximumDist
                var slerpCameraToPlayer = Vector3.Slerp(worldToCameraUnit * cameraHeight, worldToPlayerUnit * cameraHeight, (distance - maxDistCamPlayer) / distance);

                //transform.position = slerpCameraToPlayer;
                Quaternion rotation = new Quaternion();
                rotation.SetFromToRotation(transform.position, slerpCameraToPlayer);

                Vector3 newCamPos = slerpCameraToPlayer;
                Quaternion newCamRotation = rotation * transform.rotation;

                transform.SetPositionAndRotation(newCamPos, newCamRotation);

                //transform.position = newCamPos;
                //transform.LookAt(world.transform);*/
            }
        
        }
    }
}
