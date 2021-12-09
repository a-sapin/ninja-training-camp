using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    float transitionTime = 0.5f;
    public void GoToMLevelSelection()
    {
        StartCoroutine(LoadLevel("LevelSelectionMenu"));
    }

    IEnumerator LoadLevel(string scene)
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(scene);
    }

}
