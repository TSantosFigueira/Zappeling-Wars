using UnityEngine;
using System.Collections;

public class QuitManager : MonoBehaviour {

    private GameObject quitPanel;
    private Animator quitAnimator;

    void Start()
    {
        quitPanel = GameObject.FindGameObjectWithTag("QuitPanel");
        quitAnimator = quitPanel.GetComponent<Animator>();
    }

    public void DisablePauseAnimation(Animator anim)
    {
        anim.SetBool("IsDisplayed", false);
    }

    public void EnablePauseAnimation(Animator anim)
    {
        anim.SetBool("IsDisplayed", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitAnimator.SetBool("IsDisplayed", true);
        }
    }

    // Na confirmação de saída, o botão "Não" foi pressionado
    public void noPressed(Animator anim)
    {
        anim.SetBool("IsDisplayed", false);
    }

    // Na confirmação de saída, o botão "Sim" foi pressionado
    public void yesPressed()
    {
        Application.Quit();
    }
}
