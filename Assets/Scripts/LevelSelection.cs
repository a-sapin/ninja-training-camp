using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private SaveLoadData saveLoadData;
    [SerializeField] private GameObject transition;

    [Header("Medals image")] [SerializeField]
    private Texture goldMedal;

    [SerializeField] private Texture silverMedal;
    [SerializeField] private Texture bronzeMedal;

    [Header("Level 1")] [SerializeField] private Text score1;
    [SerializeField] private RawImage goldMedal1;
    [SerializeField] private RawImage silverMedal1;
    [SerializeField] private RawImage bronzeMedal1;

    [Header("Level 2")] [SerializeField] private Text score2;
    [SerializeField] private RawImage goldMedal2;
    [SerializeField] private RawImage silverMedal2;
    [SerializeField] private RawImage bronzeMedal2;

    [Header("Level 3")] [SerializeField] private Text score3;
    [SerializeField] private RawImage goldMedal3;
    [SerializeField] private RawImage silverMedal3;
    [SerializeField] private RawImage bronzeMedal3;

    public void MainMenu()
    {
        StartCoroutine(goToNextScene("MainMenu"));
    }

    public void Level1()
    {
        StartCoroutine(goToNextScene("Level1"));
    }

    public void Level2()
    {
        StartCoroutine(goToNextScene("Level2"));
    }

    public void Level3()
    {
        StartCoroutine(goToNextScene("Level3"));
    }

    IEnumerator goToNextScene(String levelname)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(levelname, LoadSceneMode.Single);
    }
    
    private void Start()
    {
        //----------------level 1--------------
        TimeSpan[] medals = SaveLoadData.getMedalTimes("Level1");
        TimeSpan[] times = SaveLoadData.getTimes("Level1");
        
        score1.text = "";
        
        Array.Sort(times);
        
        foreach (TimeSpan time in times)
        {
            if (time == TimeSpan.Zero || time == TimeSpan.FromSeconds(-1)) continue;
            
            if (time < medals[2])
            {
                goldMedal1.texture = goldMedal;
            }

            if (time < medals[1])
            {
                silverMedal1.texture = silverMedal;
            }

            if (time < medals[0])
            {
                bronzeMedal1.texture = bronzeMedal;
            }
            
            score1.text = time.ToString(@"mm\:ss\:fff") + "\n" + score1.text;
        }

        if (score1.text !="")
        {
            score1.text = "Best times:\n" + score1.text;    
        }
        

        //-------------level 2-----------------
        medals = SaveLoadData.getMedalTimes("Level2");
        times = SaveLoadData.getTimes("Level2");
        
        score2.text = "";
        
        Array.Sort(times);
        
        foreach (TimeSpan time in times)
        {
            if (time == TimeSpan.Zero || time == TimeSpan.FromSeconds(-1)) continue;
            
            
            if (time < medals[2])
            {
                goldMedal2.texture = goldMedal;
            }

            if (time < medals[1])
            {
                silverMedal2.texture = silverMedal;
            }

            if (time < medals[0])
            {
                bronzeMedal2.texture = bronzeMedal;
            }
            
            score2.text = time.ToString(@"mm\:ss\:fff") + "\n" + score2.text;
        }
        
        
        if (score2.text !="")
        {
            score2.text = "Best times:\n" + score2.text;    
        }
        
        //-----------------level 3---------------------
        medals = SaveLoadData.getMedalTimes("Level3");
        times = SaveLoadData.getTimes("Level3");
        
        score3.text = "";
        
        Array.Sort(times);

        foreach (TimeSpan time in times)
        {
            if (time == TimeSpan.Zero || time == TimeSpan.FromSeconds(-1)) continue;
            
            if (time < medals[2])
            {
                goldMedal3.texture = goldMedal;
            }

            if (time < medals[1])
            {
                silverMedal3.texture = silverMedal;
            }

            if (time < medals[0])
            {
                bronzeMedal3.texture = bronzeMedal;
            }
            
            score3.text = time.ToString(@"mm\:ss\:fff") + "\n" + score3.text;
        }
        
        
        if (score3.text !="")
        {
            score3.text = "Best times:\n" + score3.text;    
        }
        
        Time.timeScale = 1;
    }
}