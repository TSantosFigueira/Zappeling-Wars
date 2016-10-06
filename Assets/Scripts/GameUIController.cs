using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    //public GameObject PauseUI;
    //public GameObject PauseButton;
    //public GameObject GameOverUI;
    //public GameObject Hud;

    //public Text scoreText;
    //public Text highScoreText;

    //public Text gameScore;
    //public Text gameHighScore;

    //private bool paused = false;

    //void Start()
    //{
    //    PauseUI.SetActive(false);
    //    GameOverUI.SetActive(false);
    //}

    //void Update()
    //{
    //    gameScore.text = PlayerController.coins.ToString();
    //    gameHighScore.text = PlayerPrefs.GetInt("highScore").ToString();

    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Pause();
    //    }
    //}

    //public void Pause()
    //{

    //    paused = !paused;

    //    if (paused)
    //    {
    //        GameOverUI.SetActive(false);
    //        PauseUI.SetActive(true);
    //        PauseButton.SetActive(false);
    //        Time.timeScale = 0;
    //    }

    //    if (!paused)
    //    {

    //        PauseUI.SetActive(false);
    //        PauseButton.SetActive(true);
    //        Time.timeScale = 1;
    //    }

    //}

    //public void Restart()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene("Game");
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //public void MainMenu()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene("StartMenu");
    //}

    //void OnGUI()
    //{
    //    if (PlayerController.dead == true)
    //    {
    //        GameOverUI.SetActive(true);
    //        Hud.SetActive(false);
    //        showScore();
    //    }
    //}

    //public void showScore()
    //{
    //    var score = 0;
    //    scoreText.text = PlayerController.coins.ToString();
    //    score = PlayerPrefs.GetInt("highScore");
    //    highScoreText.text = score.ToString();
    //}

}
