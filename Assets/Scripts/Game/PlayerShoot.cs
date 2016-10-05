﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class PlayerShoot : NetworkBehaviour {

    public int damage = 25;
    public GameObject bullet;
    private int range = 200;
    [SerializeField]
    private Transform playerTransform;
    private RaycastHit2D hit;
    private GameObject clone;

	// Update is called once per frame
	void Update () {
        CheckIfShooting();
	}

    void CheckIfShooting()
    {
        if (!isLocalPlayer)
            return;
        else
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Shoot();
    }

    void Shoot()
    {
        clone = Instantiate(bullet, playerTransform.position, playerTransform.rotation) as GameObject;
        clone.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 1000));

        hit = Physics2D.Raycast(playerTransform.TransformPoint(0, 0, 0.5f), playerTransform.forward, range);
        if(hit.collider != null) {
            Debug.Log(hit.transform.tag);

            if(hit.transform.tag == "Player")
            {
                string Identity = hit.transform.name;
                CmdTellServer_Who_Was_Shot(Identity, damage);
            }
        }
    }

    [Command]
    void CmdTellServer_Who_Was_Shot(string uniqueID, int damage)
    {
        GameObject go = GameObject.Find(uniqueID);
        go.GetComponent<PlayerHealth>().DeductHealth(damage);
    }
}
