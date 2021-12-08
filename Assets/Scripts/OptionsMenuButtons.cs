using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuButtons : MonoBehaviour
{
    float transitionTime = 0.43f;

    int sceneID;

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void GoBackToMainMenu()
    {
        sceneID = 1;
        StartCoroutine(LoadLevel(sceneID));
    }

    IEnumerator LoadLevel(int levelID)
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelID);
    }

}
