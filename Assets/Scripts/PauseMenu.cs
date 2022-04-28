using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false;
    [HideInInspector] public bool statutPause = false;
    private Dialogue statutDialog;
    public GameObject pauseMenuUI;
    public Animator animator;
    public GameObject musicPlayerInScene;
    public ControllerInMenu controller;
    float transitionTime2 = 0.6f;
    private float exMenuKeyValue=0;
    private PlayerLocomotion player;
    private VFXManager vfxManager;
    private Transition transition;
    private AudioSource[] musicPlayer;
    private PlayerManager playerManager;

    private Timer timer;
    
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        statutDialog = FindObjectOfType<Dialogue>();
        transition = FindObjectOfType<Transition>();
        vfxManager = FindObjectOfType<VFXManager>();
        musicPlayer = musicPlayerInScene.GetComponents<AudioSource>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    
    void Update()
    {
        if (Input.GetAxisRaw("Menu")>0 && exMenuKeyValue == 0)
        {
            if (GameIsPaused)
            {
                animator.SetTrigger("FadeOut");
                ResumeEchapKey();
                StartCoroutine(AnimationTimeWaitTransition());
            }
            else
            {
                statutPause = true;
                Paused();
            }
        }
        exMenuKeyValue = Input.GetAxisRaw("Menu");
    }
    
    public void Resume2Button()
    {
        StartCoroutine(TimeTransitionButton());
    }

    public void RestartButton()
    {
        StartCoroutine(TimeTransitionButtonRestart());
    }

    public void ResumeEchapKey()
    {
        StartCoroutine(RemovePauseCanvas());
    }
    private void OnDestroy()
    {
        GameIsPaused = false;
    }
    void Paused()
    {
        Time.timeScale = 0;
        timer.SendMessage("displayPowerDesc");
        pauseMenuUI.SetActive(true);
        controller.StartMove();
        playerManager.LockGameplayInput();
        GameIsPaused = true;
        PauseAllAudio();
        vfxManager.PauseAll(); //pause all vfxmanager SoundEffect
        //vfxManager.MuteAll();
    }
    
    IEnumerator TimeTransitionButton()
    {
        yield return new WaitForSecondsRealtime(transitionTime2);
        animator.SetTrigger("FadeOut");
        StartCoroutine(RemovePauseCanvas());
    }

    IEnumerator RemovePauseCanvas()
    {
        
        yield return new WaitForSecondsRealtime(0.30f);
        timer.SendMessage("hidePowerDesc");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        PlayAllAudio();
        statutPause = false;
        if(statutDialog != null)
        {
            if (statutDialog.isInDialogue == false)
            {
                playerManager.UnlockGameplayInput();
            }
        }
        else
        {
            playerManager.UnlockGameplayInput();
        }
        //vfxManager.DeMuteAll();
    }
    
    IEnumerator AnimationTimeWaitTransition()
    {
        yield return new WaitForSecondsRealtime(0.20f);
        pauseMenuUI.SetActive(false);
    }

    IEnumerator TimeTransitionButtonRestart()
    {
        yield return new WaitForSecondsRealtime(transitionTime2);
        animator.SetTrigger("FadeOut");
        transition.TransitToScene(SceneManager.GetActiveScene().name);
    }
    public void PlayAllAudio()
    {
        foreach (AudioSource audioS in musicPlayer)
        {
            audioS.UnPause();
        }
    }
    public void PauseAllAudio()
    {
        foreach (AudioSource audioS in musicPlayer)
        {
            audioS.Pause();
        }
    }
}
