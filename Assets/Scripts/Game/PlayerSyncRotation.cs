using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour {

    [SyncVar] private Quaternion SyncPlayerRotation;
    [SerializeField] private Transform playerTransform;
    private float lerpRate = 15;

	void Update ()
    {
        LerpRotation();
        TransmitRotations();
    }

    void LerpRotation()
    {
        if(!isLocalPlayer)
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, SyncPlayerRotation, Time.deltaTime * lerpRate);
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRotation)
    {
        SyncPlayerRotation = playerRotation;
    }

    [ClientCallback]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationsToServer(playerTransform.rotation);
        }
    }
}
