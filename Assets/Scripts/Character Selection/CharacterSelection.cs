using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterSelection : MonoBehaviour {

    public Sprite[] characters;      // Through this vector, we'll be able to iterate through all available characters
    public Image characterDisplay;
    
    private int lastSelected;       // Auxiliary variable to mark the lastest character chosen

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void CharacterSelected (int character) /* changes the image display according to the 
                                                     sprite positioned in the vector */
    {
        characterDisplay.sprite = characters[character];
        lastSelected = character;
    }

    public void LoadGame() /* This method avoids setting the variable in player prefs every single time the player picks
                            another character, which he/she will do very often in the first plays */
    {
        PlayerPrefs.SetInt("character", lastSelected); 
    }
}
