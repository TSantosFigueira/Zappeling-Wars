using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResultsController : NetworkBehaviour
{

    int lives;
    Animator anim;
    [SyncVar]
    bool isGameOverVar;

    public Sprite victory;
    public Sprite defeat;
    Image result;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            anim = GameObject.Find("ResultsPanel").GetComponent<Animator>();
            result = GameObject.Find("ResultImage").GetComponent<Image>();
        }
    }

    void Victory()
    {
        if (!isLocalPlayer)
        {
            anim = GameObject.Find("ResultsPanel").GetComponent<Animator>();
            result = GameObject.Find("ResultImage").GetComponent<Image>();
            result.sprite = victory;
            anim.SetBool("isGameOver", true);
        }
    }

    void Defeat()
    {
        if (isLocalPlayer)
        {
            result.sprite = defeat;
            anim.SetBool("isGameOver", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDead();
        if (isGameOverVar)
        {
            Victory();
            Defeat();
        }
    }

    [ClientCallback]
    void isPlayerDead()
    {
        if (isLocalPlayer)
        {
            lives = gameObject.GetComponent<PlayerHealth>().lives;
            if (lives <= 0)
            {
                CmdEndGame(true);
            }
        }
    }

    [Command]
    void CmdEndGame(bool isGameOver)
    {
        isGameOverVar = isGameOver;
    }
}


