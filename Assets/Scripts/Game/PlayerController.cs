using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public float fireRate;
    private SpriteRenderer sprites;
	private float nextFire;
    private bool lastFacingSide = true;

    void Start()
    {
        sprites = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
	{
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        //Normalize the inputs
        //Vector2 direction = new Vector2(x, y).normalized;
        //Move the player
        Move();
    }

	void Move ()
	{
        float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
        GetComponent<Rigidbody>().velocity = movement * speed;

        if (movement.x < 0.0f)
            sprites.flipX = false;
        else if(movement.x > 0.0f)
            sprites.flipX = true;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody>().position.y, boundary.zMin, boundary.zMax),
            0.0f); 

        //Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        /* Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, boundary.xMin, boundary.xMax);
        pos.y = Mathf.Clamp(pos.y, boundary.zMin, boundary.zMax);
        transform.position = pos; */

        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
