using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;

    private float musicTime;
    private string music;

    void Awake()
    {
        music = bgm.clip.ToString();
        if (!SceneManager.GetActiveScene().name.Contains("level") && music == PlayerPrefs.GetString("music") && !SceneManager.GetActiveScene().name.Equals("CreditScene"))
        {
            bgm.time = PlayerPrefs.GetFloat("musicTime");
        }
    }

    private void Update()
    {
        musicTime = bgm.time;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("music", music);
        PlayerPrefs.SetFloat("musicTime", musicTime);
    }
}
