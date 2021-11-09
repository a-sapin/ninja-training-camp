using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public static bool wantsToQuit = false;

    public void PlayGame()
    {
        SceneManager.LoadScene("Prototype_Scene1");
    }

    public void AccessOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
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
}
