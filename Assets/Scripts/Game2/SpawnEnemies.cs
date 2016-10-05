using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    public Transform enemyPrefab;
    public float spawnRate = 2f;
    private bool isPositionPlayer = false;
    public Transform playerTransform;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void Spawn()
    {
        Vector3 spawnPosition;
        isPositionPlayer = !isPositionPlayer;
        if (isPositionPlayer)
        {
            spawnPosition = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
        }
        else
        {
            spawnPosition = new Vector3(transform.position.x, Random.Range(-4,4), transform.position.z);
        }
        var enemyTransform = Instantiate(enemyPrefab) as Transform;
        enemyTransform.position = spawnPosition;
    }
}
