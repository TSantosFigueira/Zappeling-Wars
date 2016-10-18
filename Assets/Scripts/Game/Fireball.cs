using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Fireball : NetworkBehaviour {

    public GameObject fireball;
    public Transform spawn;
    GameObject ball;
    float timeToSpawn = 5f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("WaitWaitPele", timeToSpawn, timeToSpawn);
	}
	

    [Command]
    void CmdSpawnFireball()
    {
        ball = (GameObject)Instantiate(fireball, new Vector3(Random.Range(-0.1f, 2.5f), Random.Range(-2f, 14f), 0), Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(Random.Range(-1500f, 1500f), Random.Range(-1010f, 1000f), 0);
        NetworkServer.Spawn(ball);
        Destroy(ball, 2f);
    }

    void WaitWaitPele()
    {
        for (int i = 0; i < 2; i++)
        {
            CmdSpawnFireball();
        }
    }

}
