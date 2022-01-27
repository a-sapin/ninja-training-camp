using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInformations : MonoBehaviour
{
    [SerializeField] private Button level1Btn, level2Btn;
    [SerializeField] private GameObject score;
    [SerializeField] private Text levelName,highScore;
    [SerializeField] private GameObject goldMedal, silverMedal, bronzeMedal;
    private ScenesTransitionManager transition;
    private void Start()
    {
        score.SetActive(false);
    }
    public void OpenLevelInfo(string level)
    {
        score.SetActive(true);
        levelName.text = level;
        int highScoreValue  = PlayerPrefs.GetInt(level + "HighScore", 999999);
        goldMedal.SetActive(highScoreValue < PlayerPrefs.GetInt(level+ "Gold", 3000));
        silverMedal.SetActive(highScoreValue < PlayerPrefs.GetInt(level + "Silver", 6000));
        bronzeMedal.SetActive(highScoreValue < PlayerPrefs.GetInt(level + "Bronze", 12000));
        if(PlayerPrefs.GetInt(level + "HighScore", -1) == -1)
        {
            highScore.text = "";
        }
        else
        {
            highScore.text = Timer.IntToStringTime(highScoreValue);
        }
        
    }
    public void CloseLevelInfo() {
        score.SetActive(false);

    }

}