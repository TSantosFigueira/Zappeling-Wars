using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings (channel = 0, sendInterval = 0.033f)]
public class PlayerSync : NetworkBehaviour {

    [SyncVar]
    private Vector2 SyncPos;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

    private Vector2 lastPos;
    private float threshold = 0.5f;

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
        if(isLocalPlayer && Vector2.Distance(myTransform.position, lastPos) > threshold)
        {
            CmdProvidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }

    }
    
	// Update is called once per frame
	void FixedUpdate ()
    {
        TransmitPosition();
	}

    void Update()
    {
        LerpPosition();
    }
}
