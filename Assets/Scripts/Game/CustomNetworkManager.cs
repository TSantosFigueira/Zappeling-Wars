using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomNetworkManager : NetworkManager
{

    public void StartUpHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string IpAddress = GameObject.Find("IpAddressText").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = IpAddress;
    }

    void OnLevelWasLoaded (int level)
    {
        if (level == 2)
            SetupMenuSceneButtons();
        else
            if(level == 3)
                SetupOtherSceneButtons();
    }

    void SetupOtherSceneButtons()
    {
         GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
         GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("Lan Button").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Lan Button").GetComponent<Button>().onClick.AddListener(StartUpHost);

        GameObject.Find("Lan Client").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Lan Client").GetComponent<Button>().onClick.AddListener(JoinGame);
    }
}
