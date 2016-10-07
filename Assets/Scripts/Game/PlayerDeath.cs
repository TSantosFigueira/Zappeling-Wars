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
        GetComponent<PlayerShoot>().enabled = false;
        GetComponentInChildren<Camera>().enabled = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
        healthScript.isDead = true;

        if (isLocalPlayer)
         {
             GetComponent<CharacterController>().enabled = false;
             // crossHair.enabled = false;
             // respawn Player
         } 
    }

    void OnDisable()
    {
        healthScript.EventDie -= DisablePlayer;
    }
}
