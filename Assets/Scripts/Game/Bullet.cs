using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    int damage = 25;
    private int damageOriginal;


    void Start()
    {
        //damage = GetComponentInParent<PlayerShoot>().damage;
        damageOriginal = damage;
    }

    public void DamageBuff(int buff){
        //Adiciona o buff ao dano
        damage = buff;
    }

    public void ReturnDamage(){
        //Retorna o dano original.
        damage = damageOriginal;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        PlayerHealth health = hit.GetComponent<PlayerHealth>();
        if(health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
