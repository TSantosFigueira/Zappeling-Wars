using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class PlayerShoot : NetworkBehaviour {

    public int damage = 25;
    public GameObject bulletPrefab;
    private int range = 2500;
    [SerializeField] private Transform playerTransform;
    private RaycastHit2D hit;
    private GameObject bullet;
    private float bulletVelocity = 1200;

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
                CmdShoot();
    }

    [Command]
    void CmdShoot()
    {
        bullet = (GameObject) Instantiate(bulletPrefab, playerTransform.position, playerTransform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, bulletVelocity));

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2);

        hit = Physics2D.Raycast(playerTransform.TransformPoint(0, 0.5f, 0), playerTransform.up, range);
        if(hit.collider != null) {
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
