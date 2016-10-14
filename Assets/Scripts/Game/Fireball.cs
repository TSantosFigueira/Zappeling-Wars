using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Fireball : NetworkBehaviour {

    public GameObject fireball;
    public Transform spawn;
    GameObject ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("WaitWaitPele");
	}

    [Command]
    void CmdSpawnFireball()
    {
        ball = (GameObject)Instantiate(fireball, spawn.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(-1.5f, 1.5f), 0));
        NetworkServer.Spawn(ball);
        Destroy(ball, .5f);
    }

    IEnumerator WaitWaitPele()
    {
        yield return new WaitForSeconds(6);
        for (int i = 0; i < 5; i++)
        {
            CmdSpawnFireball();
        }
        yield return null;
    }

}
