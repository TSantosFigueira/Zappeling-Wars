using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomNetworkManager : NetworkManager
{
    int _changedScene = 0;
    public int scenePlayerSelection = 1;
    public int sceneLobby = 3;
    public int sceneGame = 4;
    public int chosenCharacter = 0;

    void Update()
    {
        if (_changedScene == -1) return;
        else if(_changedScene == scenePlayerSelection)
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
        }
        _changedScene = -1;
    }

    public void StartUpHost()
    {
        if (!NetworkClient.active && !NetworkServer.active)
        {
            SetPort();
            NetworkManager.singleton.StartHost();
        }
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public void JoinGame()
    {
        if (!NetworkClient.active && !NetworkServer.active)
        {
            SetIPAddress();
            SetPort();
            NetworkManager.singleton.StartClient();
        }
    }

    void SetIPAddress()
    {
        string IpAddress = GameObject.Find("IpAddressText").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = IpAddress;
    }

    void OnLevelWasLoaded(int level)
    {
        _changedScene = level;
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
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

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);

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
