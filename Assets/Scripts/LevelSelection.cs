using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private SaveLoadData saveLoadData;

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
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Level1()
    {
        SceneManager.LoadScene("level1", LoadSceneMode.Single);
    }

    public void Level2()
    {
        SceneManager.LoadScene("level2", LoadSceneMode.Single);
    }

    public void Level3()
    {
        SceneManager.LoadScene("level3", LoadSceneMode.Single);
    }

    private void Start()
    {
        //level 1
        TimeSpan[] medals = SaveLoadData.getMedalTimes("level1");
        TimeSpan[] times = SaveLoadData.getTimes("level1");
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
        }

        Array.Reverse(times);
        score1.text = "Best times :\n" + times[0].ToString(@"mm\:ss\:fff") + "\n" + times[1].ToString(@"mm\:ss\:fff") +
                      "\n" + times[2].ToString(@"mm\:ss\:fff");

        //level 2
        medals = SaveLoadData.getMedalTimes("level2");
        times = SaveLoadData.getTimes("level2");
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
        }
        

        Array.Reverse(times);
        score2.text = "Best times :\n" + times[0].ToString(@"mm\:ss\:fff") + "\n" + times[1].ToString(@"mm\:ss\:fff") +
                      "\n" + times[2].ToString(@"mm\:ss\:fff");

        //level 3
        medals = SaveLoadData.getMedalTimes("level3");
        times = SaveLoadData.getTimes("level3");
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
        }

        Array.Reverse(times);
        score3.text = "Best times :\n" + times[0].ToString(@"mm\:ss\:fff") + "\n" + times[1].ToString(@"mm\:ss\:fff") +
                      "\n" + times[2].ToString(@"mm\:ss\:fff");
        Time.timeScale = 1;
    }
}