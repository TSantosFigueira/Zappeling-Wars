using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSync : NetworkBehaviour {

    [SyncVar] private Vector3 SyncPos;
    [SyncVar] private bool playerSprite;
    [SerializeField] Transform myTransform;
    float lerpRate = 15;
    public SpriteRenderer sprite;
    private Vector3 lastPos;

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, SyncPos, Time.deltaTime * lerpRate);
            sprite.flipX = playerSprite;
        }
    }

    [Command]
    void CmdProvidePositionToServer (Vector3 pos, bool actualSprite)
    {
        SyncPos = pos;
        playerSprite = actualSprite; 
    }
	
    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer)
        {
            bool isFlipped = sprite.flipX;
            CmdProvidePositionToServer(myTransform.position, isFlipped);
            lastPos = myTransform.position;
        }
    }
  
    void Update()
    {
        LerpPosition();
        TransmitPosition();       
    }
}
