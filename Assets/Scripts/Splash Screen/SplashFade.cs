using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashFade : MonoBehaviour
{
    public Image splashImage;
    public string loadLevel;

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.5f);

        FadeIn();
        yield return new WaitForSeconds(1.5f);
        FadeOut();
        yield return new WaitForSeconds(1.5f); // tempo para carregar nova fase
        SceneManager.LoadScene(loadLevel);
    }

    void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.7f, 1.5f, false);
    }
}
