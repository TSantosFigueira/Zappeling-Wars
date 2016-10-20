using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : NetworkBehaviour {

    public string jogoParaCarregar;
    public string menuPrincipal;
    //Carrega a primeira fase

	public void playGame()
    {
        SceneManager.LoadScene(jogoParaCarregar);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(menuPrincipal);
    }

    public void disconnectFromGame()
    {
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene(menuPrincipal);
    }
}
