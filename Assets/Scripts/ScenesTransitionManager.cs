using UnityEngine;

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
