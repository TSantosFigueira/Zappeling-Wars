using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour {

    [SyncVar] private string PlayerUniqueIdentity;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }

    // Use this for initialization
    void Awake ()
    {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(myTransform.name == "" || myTransform.name == "FirstPlayer(Clone)" || myTransform.name == "SecondPlayer(Clone)")
        {
            SetIdentity();
        }
	}

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdSendIndentityToServer(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = PlayerUniqueIdentity;
        } else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        string UniqueName = "Player " + playerNetID.ToString();
        return UniqueName;
    }

    [Command]
    void CmdSendIndentityToServer (string name)
    {
        PlayerUniqueIdentity = name;
    }
}
