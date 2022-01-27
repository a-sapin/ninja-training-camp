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
    private string level1 = "Level1";
    private string level2 = "Level2";
    private void Start()
    {
        transition = FindObjectOfType<Transition>();
    }
    public void GoToMenu()
    {
        transition.TransitToScene(menu);
    }
    public void GoToOptions()
    {
        transition.TransitToScene(option);
    }
    public void GoToCredits()
    {
        transition.TransitToScene(credits);
    }
    public void GoToLevelSelection()
    {
        transition.TransitToScene(selection);
    }
    public void GoToLevel1()
    {
        transition.TransitToScene(level1);
    }
    public void GoToLevel2()
    {
        transition.TransitToScene(level2);
    }
    public void Quit()
    {
        transition.TransitToScene(null);
        Invoke(nameof(DelayedQuit), transition.TransitionTime);
    }
    private void DelayedQuit()
    {
        Application.Quit();
    }

}
