using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuButtons : MonoBehaviour
{
    public Slider volume;
    float transitionTime = 0.43f;

    [SerializeField] private GameObject transition;

    int sceneID;

    private void Start()
    {
        volume.value = PlayerPrefs.GetFloat("volume",1);
        volume.onValueChanged.AddListener(ChangeVolume);
    }
    public void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat("volume",value);
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
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(levelID, LoadSceneMode.Single);
    }

}
