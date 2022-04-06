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
    public void GoToLevel(string level)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = SceneManager.GetSceneByName(level).buildIndex;
        if (level == "Level1")
        {
            PlayerPrefs.SetInt("playDialogue1", currentIndex == nextIndex ? 0 : 1);
        }
        else if (level == "Level2")
        {
            PlayerPrefs.SetInt("playDialogue2", currentIndex == nextIndex ? 0 : 1);
        }
        else if (level == "Level3")
        {
            PlayerPrefs.SetInt("playDialogue3", currentIndex == nextIndex ? 0 : 1);
        }
        else if (level == "Level4")
        {
            PlayerPrefs.SetInt("playDialogue4", currentIndex == nextIndex ? 0 : 1);
        }
        transition.TransitToScene(level);
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
