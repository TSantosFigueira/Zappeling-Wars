using UnityEngine;
using System.Collections;

public class isMobile : MonoBehaviour {

    // Use this for initialization

    void Start () {
#if UNITY_ANDROID
        gameObject.SetActive(true);
#elif UNITY_STANDALONE
        gameObject.SetActive(false);
#endif         
    }

}
