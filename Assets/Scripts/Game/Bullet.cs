using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    int damage = 25;

    void Start()
    {
        //damage = GetComponentInParent<PlayerShoot>().damage;
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
