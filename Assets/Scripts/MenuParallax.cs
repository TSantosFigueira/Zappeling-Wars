using UnityEngine;
using System.Collections;

public class MenuParallax : MonoBehaviour {

    public float speed = 0;
    private Material mat;
    private GameObject pl;
    private float pos = 0;

	// Use this for initialization
	void Start () {

        mat = GetComponent<Renderer>().material;
        pl = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.timeScale!=0)
        {
            var side = pl.transform.localScale.x;
            pos += speed * side;

            mat.mainTextureOffset = new Vector2(pos, 0);
        }
	
	}
}
