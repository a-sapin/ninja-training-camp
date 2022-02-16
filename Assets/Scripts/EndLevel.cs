using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private string currentLevel;
    [SerializeField] private GameObject dashLost;
    [SerializeField] private GameObject doubleJumpLost;
    [SerializeField] private GameObject grappleLost;
    private float lostTime = 4f;

    [SerializeField] private GameObject dashUI;
    [SerializeField] private GameObject doubleJumpUI;
    [SerializeField] private GameObject grappleUI;

    [SerializeField] private GameObject BG;
    [SerializeField] private GameObject endUI;
    [SerializeField] private Text score;
    [SerializeField] private GameObject silver;
    [SerializeField] private GameObject bronze;
    [SerializeField] private GameObject gold;

    [SerializeField] private GameObject competencesCanvas;

    private Timer timer;
    private Transition transition;
    private PlayerManager playerManager;
    private GameObject musicPlayer;
    public AudioSource endMusicVictory;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        musicPlayer = GameObject.Find("MusicPlayer");
        transition = FindObjectOfType<Transition>();
        timer = FindObjectOfType<Timer>();
        dashLost.SetActive(false);
        doubleJumpLost.SetActive(false);
        grappleLost.SetActive(false);
        endUI.SetActive(false);
    }
    public void DisplayEnd()
    {
        timer.PauseTimer();
        playerManager.LockGameplayInput(); // lock player when transition is playing
        musicPlayer.GetComponent<AudioSource>().Stop(); //Stop MusicPlayer in scene

        transition.TransitToCanvas(endUI, competencesCanvas);
        PlayerPrefs.SetInt(currentLevel + "Finished", 1);
        if(PlayerPrefs.GetInt(currentLevel + "HighScore", 999999) > timer.Time)
        {
            PlayerPrefs.SetInt(currentLevel + "HighScore", timer.Time);
        }
        gold.SetActive(timer.Time < PlayerPrefs.GetInt(currentLevel + "Gold", 3000));
        silver.SetActive(timer.Time < PlayerPrefs.GetInt(currentLevel + "Silver", 6000));
        bronze.SetActive(timer.Time < PlayerPrefs.GetInt(currentLevel + "Bronze", 12000));
       
        score.text = Timer.IntToStringTime(timer.Time);
        Invoke(nameof(waitForMusicVictory), 0.3f);
    }
    public void DisplayDashLost()
    {
        playerManager.LockGameplayInput();
        transition.TransitToCanvas(dashLost, competencesCanvas);
        dashUI.SetActive(false);

        Invoke(nameof(HideDashLost), lostTime);
    }
    private void HideDashLost()
    {
        transition.TransitToCanvas(competencesCanvas, dashLost);
        Invoke(nameof(RestartTimer), 1.3f);
    }
    public void DisplayDoubleJumpLost()
    {
        playerManager.LockGameplayInput();
        transition.TransitToCanvas(doubleJumpLost, competencesCanvas);
        doubleJumpUI.SetActive(false);
        Invoke(nameof(HideDoubleJumpLost), lostTime);
    }
    private void HideDoubleJumpLost()
    {
        transition.TransitToCanvas(competencesCanvas, doubleJumpLost);
        Invoke(nameof(RestartTimer), 1.3f);
    }
    public void DisplayGrappleLost()
    {
        playerManager.LockGameplayInput();
        transition.TransitToCanvas(grappleLost, competencesCanvas);
        grappleUI.SetActive(false);
        Invoke(nameof(HideGrappleLost), lostTime);
    }
    private void HideGrappleLost()
    {
        transition.TransitToCanvas(competencesCanvas, grappleLost);
        Invoke(nameof(RestartTimer), 1.3f);

    }
    private void RestartTimer()
    {
        timer.RestartTimer();
        playerManager.UnlockGameplayInput(); // player actionable when timer restarts
    }
    private void waitForMusicVictory()
    {
        endMusicVictory.Play();
    }
}

