using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;

    private float musicTime;
    private string music;

    void Awake()
    {
        music = BGM.clip.ToString();
        if (!SceneManager.GetActiveScene().name.Equals("Level1") && music == PlayerPrefs.GetString("music") && !SceneManager.GetActiveScene().name.Equals("CreditScene") 
            && !SceneManager.GetActiveScene().name.Equals("Level2") && !SceneManager.GetActiveScene().name.Equals("Level3") && !SceneManager.GetActiveScene().name.Equals("Level4"))
        {
            BGM.time = PlayerPrefs.GetFloat("musicTime");
        }
    }

    private void Update()
    {
        musicTime = BGM.time;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("music", music);
        PlayerPrefs.SetFloat("musicTime", musicTime);
    }
}
