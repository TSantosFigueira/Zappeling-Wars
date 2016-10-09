using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LightsController : MonoBehaviour {

    private SpriteRenderer blink;

	// Use this for initialization
	void Start () {
        blink = gameObject.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        blink.color = new Color(blink.color.r, blink.color.g, blink.color.b, 0);
        StartCoroutine("TurnOnLights");
    }

    IEnumerator TurnOnLights()
    {
        yield return new WaitForSeconds(1);
        blink.color = new Color(blink.color.r, blink.color.g, blink.color.b, 255);
    }
}
