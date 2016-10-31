using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomNetworkManager : NetworkManager
{
    int _changedScene = -1;
    int scenePlayerSelection = 1;
    int sceneLobby = 2;
    int sceneGame = 3;
    public int chosenCharacter = 0;

    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _changedScene = arg0.buildIndex;
    }

    void Update()
    {
        if (_changedScene == -1) return;
        else if (_changedScene == scenePlayerSelection)
        {
            SetupCharacterSelectionScene();
        }
        else if (_changedScene == sceneLobby)
        {
            SetupMenuSceneButtons();
        }
        else if (_changedScene == sceneGame)
        {
            SetupOtherSceneButtons();
            if(NetworkServer.active)
                SpawnNetworkedItems();
        }
        _changedScene = -1;
    }

    public void StartUpHost()
    {
        if (NetworkClient.active || NetworkServer.active)
            return;

        SetPort();
        NetworkManager.singleton.StartHost();

    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public void JoinGame()
    {
        if (NetworkClient.active || NetworkServer.active)
            return;

        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string IpAddress = GameObject.Find("IpAddressText").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = IpAddress;
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(disconnectFromGame);

        if (GameObject.Find("HomeButton"))
        {
            GameObject.Find("HomeButton").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("HomeButton").GetComponent<Button>().onClick.AddListener(disconnectFromGame);
        }
    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("NewGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("NewGame").GetComponent<Button>().onClick.AddListener(StartUpHost);

        GameObject.Find("JoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("JoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetupCharacterSelectionScene()
    {
        GameObject.Find("FirstCharacter").GetComponent<Button>().onClick.AddListener(btn1);
        GameObject.Find("SecondCharacter").GetComponent<Button>().onClick.AddListener(btn2);
    }

    public void disconnectFromGame()
    {
        GameObject.Find("SecondaryCamera").GetComponentInChildren<Camera>(true).gameObject.SetActive(true);
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("01 Menu");
    }

    void SpawnNetworkedItems()
    {
        GameObject FireballSpawner = Instantiate(Resources.Load("Game/FireballSpawner", typeof(GameObject))) as GameObject;
        GameObject PowerUpManager = Instantiate(Resources.Load("Game/PowerUpManager", typeof(GameObject))) as GameObject;
        
        NetworkServer.Spawn(FireballSpawner);
        NetworkServer.Spawn(PowerUpManager);
        NetworkServer.SpawnObjects();
    }

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        // Debug.Log("server add with message " + selectedClass);

        if (selectedClass == 0)
        {
            GameObject player = Instantiate(Resources.Load("Characters/FirstPlayer", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        if (selectedClass == 1)
        {
            GameObject player = Instantiate(Resources.Load("Characters/SecondPlayer", typeof(GameObject))) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public void btn1()
    {
        chosenCharacter = 0;
    }

    public void btn2()
    {
        chosenCharacter = 1;
    }
}
