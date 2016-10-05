using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //velocidade da nave
    public Vector2 speed = new Vector2(10f, 10f);
    private Vector2 movement;
    private Rigidbody2D body;

    // Use this for initialization
    void Start () {

        body = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        //Receber eixos de movimentação
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        //Botão de Ataque da nave
        if (Input.GetButtonDown("Fire1"))
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                weapon.Attack();
            }
        }

        //movimento de acordo com a direção dos eixos
        movement = new Vector2(inputX * speed.x, inputY * speed.y);

    }

    void FixedUpdate()
    {
        // 5 - Move the game object
        body.velocity = movement;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var damagePlayer = false;
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
                enemyHealth.Damage(enemyHealth.hp);
                damagePlayer = true;
            }
            if (damagePlayer)
            {
                HealthScript playerHealth = GetComponent<HealthScript>();
                if (playerHealth != null)
                {
                    playerHealth.Damage(1);
                }
            }
        }
    }
}
