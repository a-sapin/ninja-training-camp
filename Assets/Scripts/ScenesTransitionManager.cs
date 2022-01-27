using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesTransitionManager : MonoBehaviour
{
    private Transition transition;

    private string menu = "MainMenu";
    private string option = "OptionsMenu";
    private string credits = "CreditScene";
    private string selection = "LevelSelectionMenu";
    private void Start()
    {
        transition = FindObjectOfType<Transition>();
    }
    public void GoToMenu()
    {
        Time.timeScale = 1;
        transition.TransitToScene(menu);
    }
    public void GoToOptions()
    {
        Time.timeScale = 1;
        
        transition.TransitToScene(option);
    }
    public void GoToCredits()
    {
        Time.timeScale = 1;
        transition.TransitToScene(credits);
    }
    public void GoToLevelSelection()
    {
        Time.timeScale = 1;
        transition.TransitToScene(selection);
    }
    public void GoToLevel(string level)
    {
        Time.timeScale = 1;
        transition.TransitToScene(level);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        transition.TransitToScene(null);
        Invoke(nameof(DelayedQuit), transition.TransitionTime);
    }
    private void DelayedQuit()
    {
        Application.Quit();
    }

}
