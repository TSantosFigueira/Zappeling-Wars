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
    private Vector3 leftSpawnPosition;
    private Vector3 rightSpawnPosition;
    public float fireRate = 1f; //Variavel usada para controlar a taxa de ataque do personagem
    public float fireRateSpecial = 4f; //Variavel usada para controlar a taxa de ataque especial do personagem
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
        leftSpawnPosition = gameObject.transform.position - new Vector3(2.24f, 0.04f);
        rightSpawnPosition = gameObject.transform.position + new Vector3(1.79f, -0.04f);
        if (isLocalPlayer)
        {
            fireRate -= Time.deltaTime;
            fireRateSpecial -= Time.deltaTime;
        }
        CheckIfShooting();
    }

    void CheckIfShooting()
    {
        if (!isLocalPlayer)
            return;
#if UNITY_STANDALONE
        else
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0)) && (fireRate <= 0))
            {
                CmdShoot();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && fireRateSpecial <= 0)
            {
                CmdSpecialShoot();
            }
        }
#endif
    }

    public void normalFire()
    {
       if (fireRate <= 0)
        {
            CmdShoot();
        }
    } 

    public void superFire()
    {
        if(fireRateSpecial <= 0)
        {
            CmdSpecialShoot();
        }
    }

    [Command]
    void CmdSpecialShoot()
    {
        GetComponent<Animator>().SetBool("isFiring", true);
        if (GetComponent<SpriteRenderer>().flipX)
        {
            bullet = (GameObject)Instantiate(specialBulletPrefab, rightSpawnPosition + new Vector3(1, 0, 0), Quaternion.identity);

            if (GetComponent<PowerUps>().weaponBuff)
            {
                int buffDamage = GetComponent<PowerUps>().damageBuff;
                bullet.GetComponent<Bullet>().DamageBuff(buffDamage);
            }

            bullet.GetComponent<Rigidbody>().velocity = Vector2.right * 20;
            fireRateSpecial = 4;
        }
        else
        {
            bullet = (GameObject)Instantiate(specialBulletPrefab, leftSpawnPosition + new Vector3(-1, 0, 0), Quaternion.Euler(0, 180, 0));

            if (GetComponent<PowerUps>().weaponBuff)
            {
                int buffDamage = GetComponent<PowerUps>().damageBuff;
                bullet.GetComponent<Bullet>().DamageBuff(buffDamage);
            }

            //bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Rigidbody>().velocity = Vector2.left * 20;
            fireRateSpecial = 4;
        }
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2f);
        StartCoroutine("WaitForEndAnimation");
    }

    [Command]
    void CmdShoot()
    {
        GetComponent<Animator>().SetBool("isFiring", true);
        if (GetComponent<SpriteRenderer>().flipX)
        { // Right Shoot
            bullet = (GameObject)Instantiate(bulletPrefab, rightSpawnPosition, Quaternion.identity);

            if (GetComponent<PowerUps>().weaponBuff)
            {
                int buffDamage = GetComponent<PowerUps>().damageBuff;
                bullet.GetComponent<Bullet>().DamageBuff(buffDamage);
            }

            bullet.GetComponent<Rigidbody>().velocity = Vector2.right * 20;
            fireRate = 1;
        }
        else
        {  // Left Shoot
            bullet = (GameObject)Instantiate(bulletPrefab, leftSpawnPosition, Quaternion.Euler(0, 180, 0));

            if (GetComponent<PowerUps>().weaponBuff)
            {
                int buffDamage = GetComponent<PowerUps>().damageBuff;
                bullet.GetComponent<Bullet>().DamageBuff(buffDamage);
            }

            // bullet.GetComponent<SpriteRenderer>().flipX = true;
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
