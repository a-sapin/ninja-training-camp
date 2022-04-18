using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformations : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private Text levelName, levelName2, highScore, goldTime, silverTime, bronzeTime;
    [SerializeField] private GameObject goldMedal, silverMedal, bronzeMedal, grapple, dash, doubleJump;
    private ScenesTransitionManager transition;
    private Animator animator;
    private void Start()
    {
        score.SetActive(false);
        SetHighScoreValue();
        animator = score.GetComponent<Animator>();
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
        
        PlayerPrefs.SetString("Level2Powers", "Dash,DoubleJump,Grapple");
        
        PlayerPrefs.SetInt("Level3Gold", 45000);
        PlayerPrefs.SetInt("Level3Silver", 70000);
        PlayerPrefs.SetInt("Level3Bronze",120000);
        
        PlayerPrefs.SetString("Level3Powers", "Dash,DoubleJump,Grapple");
    }
    public void OpenLevelInfo(string level)
    {
        score.SetActive(true);
        levelName.text = levelName2.text = level;
        int highScoreValue  = PlayerPrefs.GetInt(level + "HighScore", 999999);
        
        int goldtime = PlayerPrefs.GetInt(level + "Gold", 30000);
        goldTime.text = Timer.IntToStringTime(goldtime);
        goldMedal.SetActive(highScoreValue < goldtime);
        
        int silvertime = PlayerPrefs.GetInt(level + "Silver", 60000);
        silverTime.text = Timer.IntToStringTime(silvertime);
        silverMedal.SetActive(highScoreValue < silvertime);

        int bronzetime = PlayerPrefs.GetInt(level + "Bronze", 120000);
        bronzeTime.text = Timer.IntToStringTime(bronzetime);
        bronzeMedal.SetActive(highScoreValue < bronzetime);
        
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
        animator.SetTrigger("FadeOut");
        StartCoroutine(waitAndClose());
    }

    private IEnumerator waitAndClose()
    {
        yield return new WaitForSeconds(0.1f);
        score.SetActive(false);
    }


}
