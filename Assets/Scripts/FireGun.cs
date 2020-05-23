using UnityEngine;
using UnityEngine.Networking;

public abstract class FireGun : NetworkBehaviour {
    [Command]
    public abstract void CmdFire();
}