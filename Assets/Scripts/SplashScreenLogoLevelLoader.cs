using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLogoLevelLoader : MonoBehaviour
{
    public float transitionTime = 7f;

    private void Awake()
    {
        Screen.fullScreen = intToBool(PlayerPrefs.GetInt("fullscreen", 1));
        PlayerPrefs.SetFloat("musicTime", 0f);
    }

    private bool intToBool(int i)
    {
        return i == 1;
    }

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
