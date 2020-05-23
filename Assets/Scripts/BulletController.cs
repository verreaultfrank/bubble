using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{

    [SyncVar] private Vector3 currentPosition = Vector3.zero;
    [SyncVar] private Quaternion currentRotation = Quaternion.identity;

    [ClientRpc]
    private void RpcSyncPositionWithClients(Vector3 positionToSync) {
        currentPosition = positionToSync;
    }

    [ClientRpc]
    private void RpcSyncRotationWithClients(Quaternion rotationToSync) {
        currentRotation = rotationToSync;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        //Update the posisitions for the different clients
        if (isServer) {
            RpcSyncPositionWithClients(this.transform.position);
            RpcSyncRotationWithClients(this.transform.rotation);
        }
    }

    private void LateUpdate() {
        if (!isServer) {
            this.transform.position = currentPosition;
            this.transform.rotation = currentRotation;
        }
    }
}
