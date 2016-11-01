using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    private int health = 100;
    public RectTransform healthBar;
    public int lives;

    private bool isShield = false;
    private int shieldCount;

    [SyncVar(hook = "OnHealthChanged")]
    int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = health;
        lives = 1;
    }

    public void StartShield()
    {
        isShield = true;
        shieldCount = 3;
    }

    public void FinishShield()
    {
        isShield = false;
    }

    public void TakeDamage(int amount)
    {

        GetComponent<Animator>().SetBool("isHit", true);
        StartCoroutine("setFalse");

        if (!isServer) return;

        if (isShield)
        {
            shieldCount -= 1;
            if (shieldCount <= 0)
                isShield = false;
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = health;
            RpcRespawn();
        }
    }

    IEnumerator setFalse()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<Animator>().SetBool("isHit", false);
    }

    void OnHealthChanged(int health)
    {
        healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            lives -= 1;
            transform.position = Vector3.zero;
        }
    }
}
