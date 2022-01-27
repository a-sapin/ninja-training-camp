using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    [Header("Timer")] 
    [SerializeField] private Text timerText;
    public int Time { get; private set; }
    IEnumerator timer;

    private void Start()
    {
        RestartTimer();
    }
    internal static string IntToStringTime(int value)
    {
        return (int)(value/100) + ":" + (value % 100);
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

    IEnumerator calculTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            timerText.text = IntToStringTime(Time);
            Time++;
        }
    }
    
}