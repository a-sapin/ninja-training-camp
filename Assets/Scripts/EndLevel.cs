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
    private PlayerLocomotion player;
    private GameObject musicPlayer;
    public AudioSource endMusicVictory;
    private void Start()
    {
        musicPlayer = GameObject.Find("MusicPlayer");
        player = FindObjectOfType<PlayerLocomotion>();
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
        player.canMove = false;
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
        StartCoroutine(waitForMusicVictory());
    }
    public void DisplayDashLost()
    {
        player.canMove = false;
        transition.TransitToCanvas(dashLost, competencesCanvas);
        dashUI.SetActive(false);

        Invoke(nameof(HideDashLost), lostTime);
    }
    private void HideDashLost()
    {
        player.canMove = true;
        transition.TransitToCanvas(competencesCanvas, dashLost);
        Invoke(nameof(RestartTimer), 1.3f);
    }
    public void DisplayDoubleJumpLost()
    {
        player.canMove = false;
        transition.TransitToCanvas(doubleJumpLost, competencesCanvas);
        doubleJumpUI.SetActive(false);
        Invoke(nameof(HideDoubleJumpLost), lostTime);
    }
    private void HideDoubleJumpLost()
    {
        player.canMove = true;
        transition.TransitToCanvas(competencesCanvas, doubleJumpLost);
        Invoke(nameof(RestartTimer), 1.3f);
    }
    public void DisplayGrappleLost()
    {
        player.canMove = false;
        transition.TransitToCanvas(grappleLost, competencesCanvas);
        grappleUI.SetActive(false);
        Invoke(nameof(HideGrappleLost), lostTime);
    }
    private void HideGrappleLost()
    {
        player.canMove = true;
        transition.TransitToCanvas(competencesCanvas, grappleLost);
        Invoke(nameof(RestartTimer), 1.3f);

    }
    private void RestartTimer()
    {
        timer.RestartTimer();
    }
    IEnumerator waitForMusicVictory()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        endMusicVictory.Play();
    }
}

