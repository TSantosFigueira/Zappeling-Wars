using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (isLocalPlayer)
            GetComponent<PlayerController>().enabled = true;
	}

}
