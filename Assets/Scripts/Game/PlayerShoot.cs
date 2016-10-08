using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class PlayerShoot : NetworkBehaviour
{

    public int damage = 25;
    public GameObject bulletPrefab;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private SpriteRenderer sprite;
    private int range = 2500;
    private RaycastHit2D hit;
    private GameObject bullet;
    private float bulletVelocity = 1200;

    // Update is called once per frame
    void Update()
    {
        CheckIfShooting();
    }

    void CheckIfShooting()
    {
        if (!isLocalPlayer)
            return;
        else
            if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        if (GetComponent<SpriteRenderer>().flipX)
        {
            bullet = (GameObject)Instantiate(bulletPrefab, rightSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = Vector2.right * 6;
        }
        else
        {
            bullet = (GameObject)Instantiate(bulletPrefab, leftSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = Vector2.left * 6;
        }

        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2);
    }
}
