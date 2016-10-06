using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundFXManager : MonoBehaviour {

    public Sprite buttonOn, buttonOff;
    private GameObject soundFxButton;

    void Awake()
    {
        soundFxButton = GameObject.FindGameObjectWithTag("SoundFxButton");
        string fx = PlayerPrefs.GetString("fx", "on");

        if (fx == "on")
        {
            soundFxButton.GetComponent<Image>().sprite = buttonOn;
        }

        else
        {
            soundFxButton.GetComponent<Image>().sprite = buttonOff;
        }

    }

    public void pressed()
    {
        string fx = PlayerPrefs.GetString("fx");
        if (fx == "on")
        {
            soundFxButton.GetComponent<Image>().sprite = buttonOff;
            PlayerPrefs.SetString("fx", "off");
        }

        else
        {
            soundFxButton.GetComponent<Image>().sprite = buttonOn;
            PlayerPrefs.SetString("fx", "on");
        }
    }
}
