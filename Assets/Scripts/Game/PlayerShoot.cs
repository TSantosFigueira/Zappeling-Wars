using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class PlayerShoot : NetworkBehaviour
{

    public int damage = 25;
    public GameObject bulletPrefab;
    public GameObject specialBulletPrefab;

    private SpriteRenderer sprite;
    public float range = 0.5f; // usada para controlar o tempo de destruição do bullet
    private GameObject bullet;
    private float fireRate = 1f; //Variavel usada para controlar a taxa de ataque do personagem
    private float fireRateSpecial = 4f; //Variavel usada para controlar a taxa de ataque especial do personagem
    private Button btnFire;
    private Button btnSuperFire;

#if UNITY_ANDROID
    void Start()
    {
        if (isLocalPlayer)
        {
            btnFire = GameObject.Find("btnFire").GetComponent<Button>();
            btnFire.onClick.AddListener(normalFire);

            btnSuperFire = GameObject.Find("btnSuperFire").GetComponent<Button>();
            btnSuperFire.onClick.AddListener(superFire);
        }    
    }
#endif

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        fireRate -= 0.02f;
        fireRateSpecial -= 0.01f;

#if UNITY_STANDALONE
        CheckIfShooting();
#endif
    }

    void CheckIfShooting()
    {
        if (isLocalPlayer)
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0)) && (fireRate <= 0))
            {
                CmdShoot();
                fireRate = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && fireRateSpecial <= 0)
            {
                CmdSpecialShoot();
                fireRateSpecial = 4;
            }
        }
    }

    public void normalFire()
    {
        if (fireRate <= 0)
        {
            CmdShoot();
            fireRate = 1;
        }
    }

    public void superFire()
    {
        if (fireRateSpecial <= 0)
        {
            CmdSpecialShoot();
            fireRateSpecial = 4;
        }
    }

    [Command]
    void CmdSpecialShoot()
    {
        GetComponent<Animator>().SetBool("isFiring", true);
        if (GetComponent<SpriteRenderer>().flipX)
        { //rightSpawnPosition + new Vector3(1, 0, 0)
            bullet = (GameObject)Instantiate(specialBulletPrefab, transform.position + transform.right, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = transform.right * 20;
        }
        else
        { //leftSpawnPosition + new Vector3(-1, 0, 0)
            bullet = (GameObject)Instantiate(specialBulletPrefab, transform.position - transform.right, Quaternion.Euler(0, 180, 0));
            //bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody>().velocity = Vector2.left * 20;
        }

        if (GetComponent<PowerUps>().weaponBuff)
        {
            int buffDamage = GetComponent<PowerUps>().damageBuff;
            bullet.GetComponent<Bullet>().DamageBuff(buffDamage);
        }

        Destroy(bullet, 2f);
        NetworkServer.Spawn(bullet);
        NetworkServer.SpawnObjects();
        StartCoroutine("WaitForEndAnimation");
    }

    [Command]
    void CmdShoot()
    {
        GetComponent<Animator>().SetBool("isFiring", true);
        if (GetComponent<SpriteRenderer>().flipX)
        { // Right Shoot (1.79f, -0.04f)
            bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = transform.right * 20;
        }
        else
        {  // Left Shoot new Vector3(2.24f, 0.04f)
            bullet = (GameObject)Instantiate(bulletPrefab, transform.position - transform.right , Quaternion.Euler(0, 180, 0));
            // bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody>().velocity = Vector2.left * 20;
        }

        if (GetComponent<PowerUps>().weaponBuff)
        {
            int buffDamage = GetComponent<PowerUps>().damageBuff;
            bullet.GetComponent<Bullet>().DamageBuff(buffDamage);
        }
        Destroy(bullet, 2f);
        NetworkServer.Spawn(bullet);
        NetworkServer.SpawnObjects();
        StartCoroutine("WaitForEndAnimation");
    }

    IEnumerator WaitForEndAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("isFiring", false);
    }
}
