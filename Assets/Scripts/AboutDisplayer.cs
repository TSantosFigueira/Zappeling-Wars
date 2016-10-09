using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AboutDisplayer : MonoBehaviour {

    public GameObject credits;
    private Animator creditsAnimator;

	// Use this for initialization
	void Start () {
        creditsAnimator = credits.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayCredits()
    {
        creditsAnimator.SetBool("Display", !creditsAnimator.GetBool("Display"));
    }
}
