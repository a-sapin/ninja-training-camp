using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer")] public TextMeshProUGUI timerText;

    [Header("Power images")] public RawImage grappleImage;
    public RawImage doubleJumpImage;
    public RawImage dashImage;


    [Header("Medal")] public RawImage bronzeImage;
    public float bronzeTime = 90;
    public RawImage silverImage;
    public float silverTime = 60;
    public RawImage goldImage;
    public float goldTime = 30;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.SetText(time.ToString(@"mm\:ss\:fff"));
    }

    void removeGrapple()
    {
        grappleImage.enabled = false;
    }

    void removeDoubleJump()
    {
        doubleJumpImage.enabled = false;
    }

    void removeDash()
    {
        dashImage.enabled = false;
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

    IEnumerator ShurikenTransition(Boolean reset)
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 0.75)
        {
            t += Time.fixedDeltaTime / 10;
            transform.position = Vector3.Lerp(currentPos, new Vector3(-(Screen.width + 100), 540, 0), t);
            yield return null;
        }

        if (reset) {
            t = 0f;
            while (t < 0.8)
            {
                t += Time.fixedDeltaTime / 10;
                transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1 - t);
                yield return null;
            }


            transform.position = new Vector3(960, 540, 0);
            transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1);

            Time.timeScale = 1;
        }

        else {
            timerText.color = new Color(0, 0, 0, 0);
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

            timerText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 - 100);
            timerText.transform.SetAsLastSibling();

            t = 0f;
            while (t < 0.5)
            {
                t += Time.fixedDeltaTime / 10;
                timerText.color = new Color(1, 1, 1, t);
                goldImage.color = new Color(1, 1, 1, t);
                silverImage.color = new Color(1, 1, 1, t);
                bronzeImage.color = new Color(1, 1, 1, t);

                yield return null;
            }
        }
    }
}