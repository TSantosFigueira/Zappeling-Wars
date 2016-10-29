using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class Spawner : NetworkBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        CheckIfShooting();
    }

    void CheckIfShooting()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CmdShoot();
            }
        }
    }

    [Command]
    void CmdShoot()
    {
        bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = transform.right * 20;

        Destroy(bullet, 2f);
        NetworkServer.Spawn(bullet);
    }

}
