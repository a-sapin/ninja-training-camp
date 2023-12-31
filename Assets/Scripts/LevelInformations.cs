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
        PlayerPrefs.SetInt("Level1Gold", 50000);
        PlayerPrefs.SetInt("Level1Silver", 80000);
        PlayerPrefs.SetInt("Level1Bronze",120000);
        
        PlayerPrefs.SetString("Level1Powers", "Dash,DoubleJump,Grapple");

        PlayerPrefs.SetInt("Level2Gold", 30000);
        PlayerPrefs.SetInt("Level2Silver", 50000);
        PlayerPrefs.SetInt("Level2Bronze", 80000);
        
        PlayerPrefs.SetString("Level2Powers", "Dash,DoubleJump,Grapple");
        
        PlayerPrefs.SetInt("Level3Gold", 150000);
        PlayerPrefs.SetInt("Level3Silver", 190000);
        PlayerPrefs.SetInt("Level3Bronze",260000);

        PlayerPrefs.SetString("Level3Powers", "Grapple,Dash,DoubleJump");

        PlayerPrefs.SetInt("Level4Gold", 80000);
        PlayerPrefs.SetInt("Level4Silver", 120000);
        PlayerPrefs.SetInt("Level4Bronze", 160000);

        PlayerPrefs.SetString("Level4Powers", "Grapple,DoubleJump,Dash");
    }
    public void OpenLevelInfo(string level)
    {
        score.SetActive(true);
        levelName.text = levelName2.text = level;
        int highScoreValue  = PlayerPrefs.GetInt(level + "HighScore", 999999);
        
        int goldtime = PlayerPrefs.GetInt(level + "Gold", 30000);
        goldTime.text = Timer.MilliSecondsToString(goldtime);
        goldMedal.SetActive(highScoreValue < goldtime);
        
        int silvertime = PlayerPrefs.GetInt(level + "Silver", 60000);
        silverTime.text = Timer.MilliSecondsToString(silvertime);
        silverMedal.SetActive(highScoreValue < silvertime);

        int bronzetime = PlayerPrefs.GetInt(level + "Bronze", 120000);
        bronzeTime.text = Timer.MilliSecondsToString(bronzetime);
        bronzeMedal.SetActive(highScoreValue < bronzetime);
        
        if(PlayerPrefs.GetInt(level + "HighScore", -1) == -1)
        {
            highScore.text = "";
        }
        else
        {
            highScore.text = Timer.MilliSecondsToString(highScoreValue);
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
