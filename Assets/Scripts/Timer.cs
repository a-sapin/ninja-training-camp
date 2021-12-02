using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
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


    [Header("Medal")] 
    [SerializeField] private RawImage bronzeImage;
    [SerializeField] private float bronzeTime = 90;
    [SerializeField] private RawImage silverImage;
    [SerializeField] private float silverTime = 60;
    [SerializeField] private RawImage goldImage;
    [SerializeField] private float goldTime = 30;
    
    [Header("Ending")] 
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject continueButton;
    
    private float timer;

    // Update is called once per frame
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

    IEnumerator DisplayScore()
    {
        SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.5f);
        
        Time.timeScale = 0;
        float t = 0;
        continueButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Total time :\n" + timerText.text;

        if (timer < goldTime)
        {
            goldImage.gameObject.SetActive(true);
        }
        else if (timer < silverTime)
        {
            silverImage.gameObject.SetActive(true);
        }
        else if (timer < bronzeTime)
        {
            bronzeImage.gameObject.SetActive(true);
        }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    
    
    IEnumerator DisplayRemovedPower(RawImage image)
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