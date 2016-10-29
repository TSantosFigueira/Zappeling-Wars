using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public int damage = 25;
    //private int damageOriginal;

    void Start()
    {
        //  damageOriginal = damage;
    }

    public void DamageBuff(int buff)
    {
        //Adiciona o buff ao dano
        damage += buff;
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        PlayerHealth health = hit.GetComponent<PlayerHealth>();

        if (health)
            health.TakeDamage(damage);

        Destroy(gameObject);
    }
}
