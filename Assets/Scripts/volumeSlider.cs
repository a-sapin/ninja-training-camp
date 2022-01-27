using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    public Slider volume;
    private void Start()
    {
        volume.value = PlayerPrefs.GetFloat("volume",1);
        volume.onValueChanged.AddListener(ChangeVolume);
    }
    public void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat("volume",value);
        AudioListener.volume = value;
    }

}
