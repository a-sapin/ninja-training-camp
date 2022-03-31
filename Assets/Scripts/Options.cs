using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Volume")] [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider volumeMaster;
    [SerializeField] private Slider volumeBGM;
    [SerializeField] private Slider volumeFX;


    [Header("Dialog")] [SerializeField] private Slider dialogSpeed;

    [Header("Window")] [SerializeField] private Toggle fullscreen;
    [SerializeField] private Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Awake()
    {
        volumeMaster.value = PlayerPrefs.GetFloat("volumeMaster", 1);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
        volumeBGM.value = PlayerPrefs.GetFloat("volumeBGM", 1);
        volumeBGM.onValueChanged.AddListener(ChangeVolumeBGM);
        volumeFX.value = PlayerPrefs.GetFloat("volumeFX", 1);
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);

        dialogSpeed.value = 0.05f - PlayerPrefs.GetFloat("writeDelay", 0.02f);
        dialogSpeed.onValueChanged.AddListener(ChangeDialogSpeed);

        fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen", 1));
        fullscreen.onValueChanged.AddListener(ToggleFullscreen);

        resolutions = Screen.resolutions;
        Array.Reverse(resolutions);

        ChangeResolutionFromString();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
    }

    private void ChangeResolutionFromString()
    {
        string res = PlayerPrefs.GetString("resolution", "1920 x 1080");
        int width = Int32.Parse(res.Split('x').First().Trim());
        int height = Int32.Parse(res.Split('x').Last().Trim());
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetString("resolution", res.width + " x " + res.height);
    }

    private bool intToBool(int i)
    {
        return i == 1;
    }

    private int boolToInt(bool b)
    {
        return b ? 1 : 0;
    }

    private void ToggleFullscreen(bool fullscreen)
    {
        PlayerPrefs.SetInt("fullscreen", boolToInt(fullscreen));
        Screen.fullScreen = fullscreen;
    }

    private void ChangeVolumeMaster(float value)
    {
        PlayerPrefs.SetFloat("volumeMaster", value);
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    private void ChangeVolumeBGM(float value)
    {
        PlayerPrefs.SetFloat("volumeBGM", value);
        mixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }

    private void ChangeVolumeFX(float value)
    {
        PlayerPrefs.SetFloat("volumeFX", value);
        mixer.SetFloat("FXVolume", Mathf.Log10(value) * 20);
    }

    private void ChangeDialogSpeed(float value)
    {
        PlayerPrefs.SetFloat("writeDelay", 0.05f - value);
    }
}