using UnityEngine;
using UnityEngine.Networking;

public abstract class MeleeWeapon : NetworkBehaviour {
    [Command]
    public abstract void CmdHit();
}
