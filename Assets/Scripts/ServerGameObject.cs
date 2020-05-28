using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

abstract public class ServerGameObject : NetworkBehaviour {
    [SyncVar] public Vector3 currentPosition = Vector3.zero;
    [SyncVar] public Quaternion currentRotation = Quaternion.identity;
    [SyncVar] public Vector3 currentScale;

    [ClientRpc]
    protected void RpcSyncPositionWithClients(Vector3 positionToSync) {
        currentPosition = positionToSync;
    }

    [ClientRpc]
    protected void RpcSyncRotationWithClients(Quaternion rotationToSync) {
        currentRotation = rotationToSync;
    }

    [ClientRpc]
    protected void RpcSyncScaleWithClients(Vector3 scaleToSync) {
        currentScale = scaleToSync;
    }
}