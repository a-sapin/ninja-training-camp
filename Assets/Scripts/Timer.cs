using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private SaveLoadData saveLoadData;
    
    [Header("Timer")] 
    [SerializeField] private Text timerText;

    [Header("Powers")] 
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private RawImage grappleImage;
    [SerializeField] private RawImage removeGrappleImage;
    [SerializeField] private RawImage doubleJumpImage;
    [SerializeField] private RawImage removeDoubleJumpImage;
    [SerializeField] private RawImage dashImage;
    [SerializeField] private RawImage removeDashImage;

    private TimeSpan[] medalTime = new TimeSpan[3];

    [Header("Medal")] 
    [SerializeField] private RawImage bronzeImage;
    [SerializeField] private float bronzeTime;
    [SerializeField] private RawImage silverImage;
    [SerializeField] private float silverTime;
    [SerializeField] private RawImage goldImage;
    [SerializeField] private float goldTime;
    
    [Header("Ending")] 
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject continueButton;
    
    private float timer;

    private void Awake()
    {
        medalTime[0] = TimeSpan.FromSeconds(goldTime);
        medalTime[1] = TimeSpan.FromSeconds(silverTime);
        medalTime[2] = TimeSpan.FromSeconds(bronzeTime);
        SaveLoadData.SaveTimes(medalTime);
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = time.ToString(@"mm\:ss\:fff");
    }

    void RemoveGrapple()
    {
        grappleImage.gameObject.SetActive(false);
        StartCoroutine(DisplayRemovedPower(removeGrappleImage));
    }

    void RemoveDoubleJump()
    {
        doubleJumpImage.gameObject.SetActive(false);
        StartCoroutine(DisplayRemovedPower(removeDoubleJumpImage));
    }

    void RemoveDash()
    {
        dashImage.gameObject.SetActive(false);
        StartCoroutine(DisplayRemovedPower(removeDashImage));
    }

    private IEnumerator DisplayScore()
    {
        SendMessage("AnimateTransition");
        Time.timeScale = 0;
        
        TimeSpan bestTime = SaveLoadData.getBestTime();
        TimeSpan time = TimeSpan.FromSeconds(timer);
        SaveLoadData.saveNewTime(time);
        
        yield return new WaitForSecondsRealtime(0.5f);
        
        continueButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        if (bestTime<time) scoreText.text = "New best time !\n Total time :\n" + timerText.text;
        else scoreText.text = "Total time :\n" + timerText.text;

        if (time < medalTime[0])
        {
            goldImage.gameObject.SetActive(true);
        }
        else if (time < medalTime[1])
        {
            silverImage.gameObject.SetActive(true);
        }
        else if (time < medalTime[2])
        {
            bronzeImage.gameObject.SetActive(true);
        }

        float t = 0;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / 5;
            Color fadeIn = new Color(1, 1, 1, t);
            scoreText.color = fadeIn;
            goldImage.color = fadeIn;
            silverImage.color = fadeIn;
            bronzeImage.color = fadeIn;

            yield return null;
        }
            
        t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / 4;
            Color fadeIn = new Color(1, 1, 1, t);
            continueButton.GetComponent<Image>().color = fadeIn;
            continueButton.GetComponentInChildren<Text>().color = fadeIn;
            yield return null;
        }
    }

    void DoTransition()
    {
        SendMessage("AnimateTransition");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
    private IEnumerator DisplayRemovedPower(RawImage image)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        image.gameObject.SetActive(true);
        particleSystem.gameObject.SetActive(true);
        
        float t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime;
            image.color = new Color(1, 1, 1, t);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        
        t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime/2;
            image.color = new Color(1, 1, 1, 1-t);
            yield return null;
        }
        
        //particleSystem.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }
}