using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    public Transform shotPrefab;
    public float shootingRate = 0.25f;
    private float shootCooldown;

	// Use this for initialization
	void Start () {

        shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
	}
    public void Attack ()
    {
        if (shootCooldown <= 0)
        {
            shootCooldown = shootingRate;
            var shotTransform = Instantiate(shotPrefab) as Transform;
            shotTransform.position = transform.position;
        }
    }
}
