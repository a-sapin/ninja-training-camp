using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    [SerializeField] private Text timerText;
    public int Time { get; private set; }
    IEnumerator timer;

    [SerializeField] private Text powerText1, powerText2, powerText3; 

    private void Start()
    {
        RestartTimer();
    }
    internal static string IntToStringTime(int value)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(value);
        return time.ToString(@"mm\:ss\:fff");
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
            yield return new WaitForSeconds(0.001f);
            timerText.text = IntToStringTime(Time);
            Time++;
        }
    }

    public void displayPowerDesc()
    {
        powerText1.enabled = true;
        powerText2.enabled = true;
        powerText3.enabled = true;
    }

    public void hidePowerDesc()
    {
        powerText1.enabled = false;
        powerText2.enabled = false;
        powerText3.enabled = false;
    }
    
}