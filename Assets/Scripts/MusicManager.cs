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
        if (!SceneManager.GetActiveScene().name.Contains("level") && music == PlayerPrefs.GetString("music") && !SceneManager.GetActiveScene().name.Equals("CreditScene"))
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
