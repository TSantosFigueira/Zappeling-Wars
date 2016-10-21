using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CharacterSelection : MonoBehaviour {

    public Sprite[] characters;
    public Image characterPortrait;
    public GameObject[] players;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCharacter(int num)
    {
        switch (num)
        {
            case 1:
                characterPortrait.sprite = characters[0];
                //NetworkManager.singleton.autoCreatePlayer = false;
                //RegisterPrefab(players[0]);
                //NetworkManager.singleton.playerPrefab = players[0];
                //NetworkManager.singleton.autoCreatePlayer = true;
                //Quantum
                break;
            case 2:
                characterPortrait.sprite = characters[1];
                //NetworkManager.singleton.autoCreatePlayer = false;
                //RegisterPrefab(players[1]);
                //NetworkManager.singleton.playerPrefab = players[1];
                //NetworkManager.singleton.autoCreatePlayer = true;
                break;
          /*  case 3:
                characterPortrait.sprite = characters[2];
                break;
            case 4:
                characterPortrait.sprite = characters[3];
                break; */
        }
    }

    void RegisterPrefab(GameObject prefab)
    {
        NetworkHash128 creatureAssetId = NetworkHash128.Parse("e2656f");
        ClientScene.RegisterPrefab(prefab, creatureAssetId);
    }

}
