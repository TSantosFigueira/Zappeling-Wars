using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    private AudioSource source;
    private GameObject audioButton;
    public Sprite buttonOn, buttonOff;

	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
        audioButton = GameObject.FindGameObjectWithTag("SoundButton");
        //audioButton = GetComponent<Button>();

        string music = PlayerPrefs.GetString("music", "on");
        source.loop = true;

        if (music == "on")
        {
            source.Play();
            audioButton.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            source.Play();
            source.Pause();
            audioButton.GetComponent<Image>().sprite = buttonOff;
        }
            
	}

    public void pressed()
    {
        if (source.isPlaying)
        {
            source.Pause();
            audioButton.GetComponent<Image>().sprite = buttonOff;
            PlayerPrefs.SetString("music", "off");
        }
            
        else
        {
            source.UnPause();
            audioButton.GetComponent<Image>().sprite = buttonOn;
            PlayerPrefs.SetString("music", "on");
        }
    }
}
