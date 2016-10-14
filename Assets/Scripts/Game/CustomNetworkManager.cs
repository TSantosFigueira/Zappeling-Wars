using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomNetworkManager : NetworkManager
{
    int _changedScene = 0;

    void Update()
    {
        if (_changedScene == -1) return;
        else if (_changedScene == 2)
        {
            SetupMenuSceneButtons();
        }
        else if (_changedScene == 3)
        {
            SetupOtherSceneButtons();
        }
        _changedScene = -1;
    }

    public void StartUpHost()
    {
        if(!NetworkClient.active && !NetworkServer.active)
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

    void OnLevelWasLoaded (int level)
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
}
