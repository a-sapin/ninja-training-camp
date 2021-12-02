using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLogoLevelLoader : MonoBehaviour
{
    public float transitionTime = 7f;

    // Update is called once per frame
    void Update()
    {
        LoadNextLevel();
    }
    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
