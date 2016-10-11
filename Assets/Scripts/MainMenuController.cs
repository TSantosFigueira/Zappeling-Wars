using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

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
}
