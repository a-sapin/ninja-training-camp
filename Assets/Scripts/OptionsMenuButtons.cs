using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuButtons : MonoBehaviour
{

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
