using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private string currentLevel;
    [SerializeField] private GameObject dashLost;
    [SerializeField] private GameObject doubleJumpLost;
    [SerializeField] private GameObject grappleLost;
    private float lostTime = 5f;

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
        timer.PauseTimer();
        transition.TransitToCanvas(dashLost, competencesCanvas);

        Invoke(nameof(HideDashLost), lostTime);
    }
    private void HideDashLost()
    {
        playerManager.UnlockGameplayInput();
        dashUI.SetActive(false);
        transition.TransitToCanvas(competencesCanvas, dashLost);
        Invoke(nameof(RestartTimer), 1.4f);
    }
    public void DisplayDoubleJumpLost()
    {
        playerManager.LockGameplayInput();
        timer.PauseTimer();
        transition.TransitToCanvas(doubleJumpLost, competencesCanvas);
        Invoke(nameof(HideDoubleJumpLost), lostTime);
    }
    private void HideDoubleJumpLost()
    {
        doubleJumpUI.SetActive(false);
        playerManager.UnlockGameplayInput();
        transition.TransitToCanvas(competencesCanvas, doubleJumpLost);
        Invoke(nameof(RestartTimer), 1.4f);
    }
    public void DisplayGrappleLost()
    {
        playerManager.LockGameplayInput();
        timer.PauseTimer();
        transition.TransitToCanvas(grappleLost, competencesCanvas);
        Invoke(nameof(HideGrappleLost), lostTime);
    }
    private void HideGrappleLost()
    {

        grappleUI.SetActive(false);
        playerManager.UnlockGameplayInput();
        transition.TransitToCanvas(competencesCanvas, grappleLost);
        Invoke(nameof(RestartTimer), 1.4f);
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

