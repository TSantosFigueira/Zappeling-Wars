using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class FireballController : NetworkBehaviour
{

    public int damage = 15;

    public void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;
        PlayerHealth health = hit.GetComponent<PlayerHealth>();

        if(health)
            health.TakeDamage(damage);

        StartCoroutine("killFireball");
    }

    IEnumerator killFireball()
    {
        GetComponent<Animator>().SetBool("isDestroyed", true);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

}
