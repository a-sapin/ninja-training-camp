using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public static bool wantsToQuit = false;

    int sceneIntBuildSettings;

    float transitionTime = 0.5f;

    public void PlayGame()
    {
        sceneIntBuildSettings = 4;
        StartCoroutine(LoadLevel(sceneIntBuildSettings));
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
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelID);
    }
}
