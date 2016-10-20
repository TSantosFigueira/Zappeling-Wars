using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[NetworkSettings (channel = 0, sendInterval = 0.033f)]
public class PlayerSync : NetworkBehaviour {

    [SyncVar]
    private Vector2 SyncPos;
    [SyncVar]
    private bool playerSprite;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;
    public SpriteRenderer sprite;

    private Vector2 lastPos;
    private float threshold = 0.1f;

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector2.Lerp(myTransform.position, SyncPos, Time.deltaTime * lerpRate);
            sprite.flipX = playerSprite;
        }
    }

    [Command]
    void CmdProvidePositionToServer (Vector2 pos, bool actualSprite)
    {
        SyncPos = pos;
        playerSprite = actualSprite; 
    }
	
    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer && Vector2.Distance(myTransform.position, lastPos) > threshold)
        {
            bool isFlipped = sprite.flipX;
            CmdProvidePositionToServer(myTransform.position, isFlipped);
            lastPos = myTransform.position;
        }
    }
  
    void Update()
    {
        TransmitPosition();
        LerpPosition();
    }
}
