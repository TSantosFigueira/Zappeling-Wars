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
    public float range = 0.5f; // usada para controlar o tempo de destruição do bullet
    private RaycastHit2D hit;
    private GameObject bullet;
    //public float bulletVelocity = 5000;
    public float fireRate = 1f; //Variavel usada para controlar a taxa de ataque do personagem

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        CheckIfShooting();
    }

    void CheckIfShooting()
    {
        if (!isLocalPlayer)
            return;
        else
            if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0)
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        GetComponent<Animator>().SetBool("isFiring", true);
        if (GetComponent<SpriteRenderer>().flipX)
        {
            bullet = (GameObject)Instantiate(bulletPrefab, rightSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = Vector2.right * 20;
            fireRate = 1;
        }
        else
        {
            bullet = (GameObject)Instantiate(bulletPrefab, leftSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody>().velocity = Vector2.left * 20;
            fireRate = 1;
        }
        NetworkServer.Spawn(bullet);
        Destroy(bullet, range);
        StartCoroutine("WaitForEndAnimation");
    }

    IEnumerator WaitForEndAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("isFiring", false);
    }
}
