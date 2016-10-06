using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{
    public Texture2D[] slides = new Texture2D[4];
    public float changeTime = 5.0f;
    private int currentSlide = 0;
    private float timeSinceLast = 1.0f;
    private string playIntro;

    void Awake()
    {
        playIntro = PlayerPrefs.GetString("intro", "yes");
        if (playIntro == "yes")
        {
            transform.position = new Vector3(0.5f, 0.5f, 0.0f);
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            GetComponent<GUITexture>().texture = slides[currentSlide];
            GetComponent<GUITexture>().pixelInset = new Rect(-slides[currentSlide].width / 2.0f, -slides[currentSlide].height / 2.0f, slides[currentSlide].width, slides[currentSlide].height);
            currentSlide++;
        }
    }

    void Update()
    {
        if (playIntro == "yes")
        {
            if (timeSinceLast > changeTime && currentSlide < slides.Length)
            {
                GetComponent<GUITexture>().texture = slides[currentSlide];
                GetComponent<GUITexture>().pixelInset = new Rect(-slides[currentSlide].width / 2.0f, -slides[currentSlide].height / 2.0f, slides[currentSlide].width, slides[currentSlide].height);
                timeSinceLast = 0.0f;
                currentSlide++;
            }
            timeSinceLast += Time.deltaTime;
        }

        if(currentSlide == slides.Length || playIntro == "no")
        {
            PlayerPrefs.SetString("intro", "no");
            SceneManager.LoadScene("StartMenu");
        }
    }
}