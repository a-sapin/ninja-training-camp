using System.Collections;
using UnityEngine;

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

    private Timer timer;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerLocomotion>();
        timer = FindObjectOfType<Timer>();
    }
    
    void FixedUpdate()
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
    
    public void ResumeEchapKey()
    {
        StartCoroutine(RemovePauseCanvas());
    }
    
    void Paused()
    {
        Time.timeScale = 0;
        player.canMove = false;
        pauseMenuUI.SetActive(true);
        controller.StartMove();
        GameIsPaused = true;
        audioSourceInScene.Pause();
        VFXManager vfxManager = FindObjectOfType<VFXManager>();
        vfxManager.Pause("Movement");
        vfxManager.Pause("Jump");
        vfxManager.Pause("Dash");
        vfxManager.Pause("Grapple");
        
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
        yield return new WaitForSecondsRealtime(0.30f);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        player.canMove = true;
        GameIsPaused = false;
        audioSourceInScene.Play();
        timer.SendMessage("hidePowerDesc");
    }
    
    IEnumerator AnimationTimeWaitTransition()
    {
        yield return new WaitForSecondsRealtime(0.20f);
        pauseMenuUI.SetActive(false);
    }
}
