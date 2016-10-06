using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    //Carrega a primeira fase
	public void playGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
