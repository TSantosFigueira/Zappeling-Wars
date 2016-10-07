using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class PlayerShoot : NetworkBehaviour {

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
	void Update () {
        sprite = GetComponent<SpriteRenderer>();
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
        if (sprite.flipX)
        {
            bullet = (GameObject)Instantiate(bulletPrefab, rightSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(Vector3.right * 20000 * Time.deltaTime);
        }         
        else{
            bullet = (GameObject)Instantiate(bulletPrefab, leftSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(Vector3.left * 20000 * Time.deltaTime);
        }
            
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2);

        //hit = Physics2D.Raycast(playerTransform.TransformPoint(0, 0.5f, 0), playerTransform.up, range);
        //if(hit.collider != null) {
        //    if(hit.transform.tag == "Player")
        //    {
        //        string Identity = hit.transform.name;
        //        CmdTellServer_Who_Was_Shot(Identity, damage);
        //    }
        //}
    }

    [Command]
    void CmdTellServer_Who_Was_Shot(string uniqueID, int damage)
    {
        GameObject go = GameObject.Find(uniqueID);
        go.GetComponent<PlayerHealth>().DeductHealth(damage);
    }
}
