using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    int damage;

    void Start()
    {
        damage = GetComponent<PlayerShoot>().damage;
    }

    public void OnCollisionEnter2D(Collision2D collision)
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
