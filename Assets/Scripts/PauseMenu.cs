using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator animator;
    public AudioSource audioSourceInScene;
    public ControllerInMenu controller;
    float transitionTime2 = 0.6f;
    private float exMenuKeyValue=0;
    private PlayerLocomotion player;
    private VFXManager vfxManager;
    private Transition transition;

    private Timer timer;
    
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        transition = FindObjectOfType<Transition>();
        vfxManager = FindObjectOfType<VFXManager>();
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
    
    void Paused()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        controller.StartMove();
        GameIsPaused = true;
        audioSourceInScene.Pause();
        vfxManager.PauseAll(); //pause all vfxmanager SoundEffect
        timer.SendMessage("displayPowerDesc");
    }
    
    IEnumerator TimeTransitionButton()
    {
        yield return new WaitForSecondsRealtime(transitionTime2);
        animator.SetTrigger("FadeOut");
        StartCoroutine(RemovePauseCanvas());
    }

    IEnumerator RemovePauseCanvas()
    {
        timer.SendMessage("hidePowerDesc");
        yield return new WaitForSecondsRealtime(0.30f);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        audioSourceInScene.Play();
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
}
