using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer")] 
    [SerializeField] private Text timerText;

    [Header("Power images")] 
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
    void Update()
    {
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = time.ToString(@"mm\:ss\:fff");
    }

    void removeGrapple()
    {
        grappleImage.gameObject.SetActive(false);
        StartCoroutine(fadeInOut(removeGrappleImage));
    }

    void removeDoubleJump()
    {
        doubleJumpImage.gameObject.SetActive(false);
        StartCoroutine(fadeInOut(removeDoubleJumpImage));
    }

    void removeDash()
    {
        dashImage.gameObject.SetActive(false);
        StartCoroutine(fadeInOut(removeDashImage));
    }

    void displayScore()
    {
        Time.timeScale = 0;
        StartCoroutine(ShurikenTransition(false));
    }

    void doTransition()
    {
        Time.timeScale = 0;
        StartCoroutine(ShurikenTransition(true));
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    IEnumerator ShurikenTransition(Boolean reset)
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / 5;
            transform.position = Vector3.Lerp(currentPos, new Vector3(-(Screen.width + 100), 540, 0), t);
            yield return null;
        }

        if (!reset)
        {
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

            t = 0f;
            while (t < 0.5)
            {
                t += Time.fixedDeltaTime / 10;
                Color fadeIn = new Color(1, 1, 1, t);
                scoreText.color = fadeIn;
                goldImage.color = fadeIn;
                silverImage.color = fadeIn;
                bronzeImage.color = fadeIn;

                yield return null;
            }
            
            t = 0f;
            while (t < 0.25)
            {
                t += Time.fixedDeltaTime / 10;
                Color fadeIn = new Color(1, 1, 1, t);
                continueButton.GetComponent<Image>().color = fadeIn;
                continueButton.GetComponentInChildren<Image>().color = fadeIn;
                yield return null;
            }
        }
    }

    IEnumerator fadeOut()
    {
        float t = 0f;
        while (t < 0.8)
        {
            t += Time.fixedDeltaTime / 5;
            transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1 - t);
            yield return null;
        }
        
        transform.position = new Vector3(960, 540, 0);
        transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1);

        Time.timeScale = 1;
    }
    
    IEnumerator fadeInOut(RawImage image)
    {
        image.gameObject.SetActive(true);
        
        float t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / 5;
            image.color = new Color(1, 1, 1, t);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2);
        
        t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / 2;
            image.color = new Color(1, 1, 1, 1-t);
            yield return null;
        }
        
        image.gameObject.SetActive(false);
    }
}