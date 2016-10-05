using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour
{

    public int damage = 1;

    public string objectTag;

    void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == objectTag)
        {
            HealthScript health = collider.gameObject.GetComponent<HealthScript>();

            if (health != null)
            {
                if (health != null)
                {
                    health.Damage(damage);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
