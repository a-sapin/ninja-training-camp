using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator animator;
    public AudioSource audioSourceInScene;
    float transitionTime2 = 0.6f;
    private float exMenuKeyValue=0;
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetAxisRaw("Menu")>0 && exMenuKeyValue == 0)
        {
            if (GameIsPaused)
            {
                animator.SetTrigger("FadeOut");
                ResumeEchapKey();
                StartCoroutine(AnimationTimeWaitTransition());
            }
            else
            {
                Paused();
            }

        }
        exMenuKeyValue = Input.GetAxisRaw("Menu");
    }
    public void Resume2Button()
    {
        StartCoroutine(TimeTransitionButton());
    }
    public void ResumeEchapKey()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        audioSourceInScene.Play();
    }
    void Paused()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        audioSourceInScene.Pause();
        FindObjectOfType<VFXManager>().Pause("Movement");
        FindObjectOfType<VFXManager>().Pause("Jump");
        FindObjectOfType<VFXManager>().Pause("Dash");
        FindObjectOfType<VFXManager>().Pause("Grapple");
    }
    IEnumerator TimeTransitionButton()
    {
        yield return new WaitForSecondsRealtime(transitionTime2);
        animator.SetTrigger("FadeOut");
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioSourceInScene.Play();
        yield return new WaitForSecondsRealtime(0.30f);
        pauseMenuUI.SetActive(false);
    }
    IEnumerator AnimationTimeWaitTransition()
    {
        yield return new WaitForSecondsRealtime(0.20f);
        pauseMenuUI.SetActive(false);
    }
}
