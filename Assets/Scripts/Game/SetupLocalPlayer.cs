using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets._2D;

public class SetupLocalPlayer : NetworkBehaviour {

    public Camera PlayerCamera;

	// Use this for initialization
	void Start ()
    {
        if (isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Main Camera").SetActive(false);
            PlayerCamera.enabled = true;
            //transform.parent = GetComponentInChildren<Camera>().transform;
            //Debug.Log(gameObject.GetComponentInChildren<Camera>().name);
            //GameObject.Find("Main Camera 1").SetActive(true);

        }
           
	}

}
