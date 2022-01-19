using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator animator;
    public AudioSource audioSourceInScene;

    float transitionTime = 0.43f;
    float transitionTime2 = 0.6f;

    int sceneID;
    [SerializeField] private GameObject transition;

    private float exMenuKeyValue=0;
    private PlayerLocomotion player;
    private void Start()
    {
        player = FindObjectOfType<PlayerLocomotion>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("EXXX:" + exMenuKeyValue);
        if (Input.GetAxis("Menu")>0 && exMenuKeyValue == 0)
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
        exMenuKeyValue = Input.GetAxis("Menu");
    }
    public void Resume2Button()
    {
        StartCoroutine(TimeTransitionButton());
    }
    public void ResumeEchapKey()
    {
        player.canMove = true;
        GameIsPaused = false;
        audioSourceInScene.Play();
    }
    void Paused()
    {
        player.canMove = false;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        audioSourceInScene.Pause();
        FindObjectOfType<VFXManager>().Pause("Movement");
        FindObjectOfType<VFXManager>().Pause("Jump");
        FindObjectOfType<VFXManager>().Pause("Dash");
        FindObjectOfType<VFXManager>().Pause("Grapple");
    }
    public void LoadMenu()
    {
        sceneID = 1;
        StartCoroutine(LoadLevel(sceneID));
    }
    public void QuitMenu()
    {
        Debug.Log("Quitting menu...");
        Application.Quit();
    }
    IEnumerator LoadLevel(int levelID)
    {
        yield return new WaitForSecondsRealtime(transitionTime);
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(levelID, LoadSceneMode.Single);
    }

    IEnumerator TimeTransitionButton()
    {
        yield return new WaitForSecondsRealtime(transitionTime2);
        animator.SetTrigger("FadeOut");
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioSourceInScene.Play();
        yield return new WaitForSecondsRealtime(0.30f);
        pauseMenuUI.SetActive(false);
    }
    IEnumerator AnimationTimeWaitTransition()
    {
        yield return new WaitForSecondsRealtime(0.20f);
        pauseMenuUI.SetActive(false);
    }
}
