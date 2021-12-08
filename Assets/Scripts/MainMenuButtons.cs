using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject transition;

    public static bool wantsToQuit = false;

    int sceneIntBuildSettings;

    float transitionTime = 0.5f;

    public void PlayGame()
    {
        StartCoroutine(goToNextScene("LevelSelectionMenu"));
    }

    public void AccessOptionsMenu()
    {
        sceneIntBuildSettings = 2;
        StartCoroutine(LoadLevel(sceneIntBuildSettings));
    }

    public void QuitGame()
    {
        wantsToQuit = true;
        StartCoroutine(DelayedQuit());
    }

    private IEnumerator DelayedQuit()
    {
        yield return null;
        Application.Quit();
    }
    public void AccessCredit()
    {
        sceneIntBuildSettings = 3;
        StartCoroutine(LoadLevel(sceneIntBuildSettings));
    }
    
    IEnumerator LoadLevel(int levelID)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.55f);

        SceneManager.LoadScene(levelID);
    }
    
    IEnumerator goToNextScene(String levelname)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.55f);
        SceneManager.LoadScene(levelname, LoadSceneMode.Single);
    }
}
