using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    [SerializeField] private Text timerText;
    public int Time { get; private set; }
    IEnumerator timer;

    [SerializeField] private GameObject powerText1, powerText2, powerText3; 

    private void Start()
    {
        if(FindObjectOfType<Dialogue>() ==null && FindObjectOfType<CameraTransition>() == null) RestartTimer();
    }
    internal static string CentiSecondsToString(int value)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(value * 10);
        return time.ToString(@"mm\:ss\:ff");
    }

    internal static string MilliSecondsToString(int value)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(value);
        return time.ToString(@"mm\:ss\:ff");
    }

    internal static int toRealInt(int value)
    {
        return (int) TimeSpan.FromMilliseconds(value * 10).TotalMilliseconds;
    }
    
    public void StartTimer()
    {
        Time = 0;
        timer = calculTime();
        StartCoroutine(timer);
    }
    public float PauseTimer()
    {
        if (timer != null) StopCoroutine(timer);
        return Time;
    }
    public void RestartTimer()
    {
        if (timer != null) StopCoroutine(timer);
        timer = calculTime();
        StartCoroutine(timer);
    }

    private IEnumerator calculTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            timerText.text = CentiSecondsToString(Time);
            Time++;
        }
    }

    public void displayPowerDesc()
    {
        powerText1.SetActive(true);
        powerText2.SetActive(true);
        powerText3.SetActive(true);
    }

    public IEnumerator hidePowerDesc()
    {
        powerText1.GetComponent<Animator>().SetTrigger("FadeOut");
        powerText2.GetComponent<Animator>().SetTrigger("FadeOut");
        powerText3.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSecondsRealtime(0.2f);
        
        powerText1.SetActive(false);
        powerText2.SetActive(false);
        powerText3.SetActive(false);
    }
    
}