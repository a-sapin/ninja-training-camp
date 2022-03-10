using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;

    private float musicTime;
    private string music;
    void Start()
    {
        music = BGM.clip.ToString();
        if (music == PlayerPrefs.GetString("music"))
        {
            BGM.time = PlayerPrefs.GetFloat("musicTime");
        }
        else
        {
            PlayerPrefs.SetFloat("musicTime", 0f);
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
