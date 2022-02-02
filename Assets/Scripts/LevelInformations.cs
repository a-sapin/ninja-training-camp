using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformations : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private Text levelName,levelName2,highScore;
    [SerializeField] private GameObject goldMedal, silverMedal, bronzeMedal, grapple, dash, doubleJump;
    private ScenesTransitionManager transition;
    private void Start()
    {
        score.SetActive(false);
        SetHighScoreValue();
    }
    public void SetHighScoreValue()
    {
        PlayerPrefs.SetInt("Level1Gold", 45000);
        PlayerPrefs.SetInt("Level1Silver", 70000);
        PlayerPrefs.SetInt("Level1Bronze",120000);
        
        PlayerPrefs.SetString("Level1Powers", "Dash,DoubleJump,Grapple");

        PlayerPrefs.SetInt("Level2Gold", 45000);
        PlayerPrefs.SetInt("Level2Silver", 70000);
        PlayerPrefs.SetInt("Level2Bronze", 120000);
        
        PlayerPrefs.SetString("Level2Powers", "Dash,DoubleJump");
    }
    public void OpenLevelInfo(string level)
    {
        score.SetActive(true);
        levelName.text = levelName2.text = level;
        int highScoreValue  = PlayerPrefs.GetInt(level + "HighScore", 999999);
        goldMedal.SetActive(highScoreValue < PlayerPrefs.GetInt(level+ "Gold", 30000));
        silverMedal.SetActive(highScoreValue < PlayerPrefs.GetInt(level + "Silver", 60000));
        bronzeMedal.SetActive(highScoreValue < PlayerPrefs.GetInt(level + "Bronze", 120000));
        if(PlayerPrefs.GetInt(level + "HighScore", -1) == -1)
        {
            highScore.text = "";
        }
        else
        {
            highScore.text = Timer.IntToStringTime(highScoreValue);
        }

        grapple.SetActive(PlayerPrefs.GetString(level+"Powers").Contains("Grapple"));
        dash.SetActive(PlayerPrefs.GetString(level+"Powers").Contains("Dash"));
        doubleJump.SetActive(PlayerPrefs.GetString(level+"Powers").Contains("DoubleJump"));
        
    }
    public void CloseLevelInfo() {
        score.SetActive(false);
    }


}