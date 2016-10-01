using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour {

    [SyncVar]
    private Vector2 SyncPos;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector2.Lerp(myTransform.position, SyncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer (Vector2 pos)
    {
        SyncPos = pos;
    }
	
    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer)
            CmdProvidePositionToServer(myTransform.position);
    }
    
	// Update is called once per frame
	void FixedUpdate ()
    {
        TransmitPosition();
        LerpPosition();
	}
}
