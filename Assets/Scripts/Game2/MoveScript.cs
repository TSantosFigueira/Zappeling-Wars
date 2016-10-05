using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

    public Vector2 speed = new Vector2(5f, 5f);
    private Vector2 movement;
    public Vector2 direction = new Vector2(-1, 0);
    private Rigidbody2D body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //movimento por direção
        movement = new Vector2(direction.x * speed.x, direction.y * speed.y);
    }
    void FixedUpdate()
    {
        // 5 - Move the game object
        body.velocity = movement;
    }
}
