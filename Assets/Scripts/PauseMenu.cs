using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

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
                ResumeEchapKey();
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
        pauseMenuUI.SetActive(false);
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
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
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

        SceneManager.LoadScene(levelID);
    }

    IEnumerator TimeTransitionButton()
    {
        yield return new WaitForSecondsRealtime(transitionTime2);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioSourceInScene.Play();
    }
}
