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
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    private void Start()
    {
        RestartTimer();
    }
    internal static string IntToStringTime(int value)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(value * 10);
        return time.ToString(@"mm\:ss\:ff");
    }
    public void StartTimer()
    {
        Time = 0;
        timer = CalculTime();
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
        timer = CalculTime();
        StartCoroutine(timer);
    }

    private IEnumerator CalculTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            timerText.text = IntToStringTime(Time);
            Time++;
        }
    }

    public void DisplayPowerDesc()
    {
        powerText1.SetActive(true);
        powerText2.SetActive(true);
        powerText3.SetActive(true);
    }

    public IEnumerator HidePowerDesc()
    {
        powerText1.GetComponent<Animator>().SetTrigger(FadeOut);
        powerText2.GetComponent<Animator>().SetTrigger(FadeOut);
        powerText3.GetComponent<Animator>().SetTrigger(FadeOut);
        yield return new WaitForSecondsRealtime(0.2f);
        
        powerText1.SetActive(false);
        powerText2.SetActive(false);
        powerText3.SetActive(false);
    }
    
}