using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public int health = 100;
    public RectTransform healthBar;
    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    [SyncVar (hook = "OnHealthChanged")] private int currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
    }

    void CheckCondition()
    {
        if (health <= 0 && !shouldDie && !isDead)
        {
            shouldDie = true;
        }

        if (health <= 0 && shouldDie)
        {
            if (EventDie != null)
            {
                EventDie();
            }

            shouldDie = false;
        }
    }

    public void DeductHealth(int damage)
    {
        health -= damage;
    }

    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = health;
            RpcRespawn();
        }
    }

    void OnHealthChanged (int health)
    {
       healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
