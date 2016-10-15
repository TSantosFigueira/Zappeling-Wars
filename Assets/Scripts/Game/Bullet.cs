using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    int damage = 25;

    void Start() {
        Debug.Log("Damage: " + damage);
    }

    public void DamageBuff(int buff){
        //Adiciona o buff ao dano
        Debug.Log(string.Format("Dano:{0}, Buff:{1}, Total:{2}", damage, buff, damage + buff));
        damage += buff;
        
    }


    public void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        PlayerHealth health = hit.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
