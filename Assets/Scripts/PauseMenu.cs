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

    float transitionTime = 0.43f;
    float transitionTime2 = 0.6f;

    int sceneID;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void Resume2Button()
    {
        StartCoroutine(TimeTransitionButton());
    }
    public void ResumeEchapKey()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioSourceInScene.Play();
    }
    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioSourceInScene.Pause();
        FindObjectOfType<VFXManager>().Pause("Movement");
        FindObjectOfType<VFXManager>().Pause("Jump");
        FindObjectOfType<VFXManager>().Pause("Dash");
        FindObjectOfType<VFXManager>().Pause("Grapple");
    }
    public void LoadMenu()
    {
        sceneID = 1;
        StartCoroutine(LoadLevel(sceneID));
    }
    public void QuitMenu()
    {
        Debug.Log("Quitting menu...");
        Application.Quit();
    }
    IEnumerator LoadLevel(int levelID)
    {
        yield return new WaitForSecondsRealtime(transitionTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelID);
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
