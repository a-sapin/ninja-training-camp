using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLogoLevelLoader : MonoBehaviour
{
    public float transitionTime = 7f;

    private void Awake()
    {
        Screen.fullScreen = INTToBool(PlayerPrefs.GetInt("fullscreen", 1));
        PlayerPrefs.SetString("music", "");
        PlayerPrefs.SetFloat("musicTime", 0f);
        ChangeResolutionFromString();
    }
    
    private void ChangeResolutionFromString()
    {
        string res = PlayerPrefs.GetString("resolution", "1920 x 1080");
        int width = Int32.Parse(res.Split('x').First().Trim()); 
        int height = Int32.Parse(res.Split('x').Last().Trim());
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    private static bool INTToBool(int i)
    {
        return i == 1;
    }

    // Update is called once per frame
    void Start()
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
