using UnityEngine;
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
