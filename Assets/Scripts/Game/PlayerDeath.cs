using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour {

    private PlayerHealth healthScript;
    private Image crossHair;

	// Use this for initialization
	void Start () {
        healthScript = GetComponent<PlayerHealth>();
        healthScript.EventDie += DisablePlayer;
	}


    void DisablePlayer()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
        healthScript.isDead = true;

        if (isLocalPlayer)
        {
            crossHair.enabled = false;
        }
    }

    void OnDisable()
    {
        healthScript.EventDie -= DisablePlayer;
    }
}
