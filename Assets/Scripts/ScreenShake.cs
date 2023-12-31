using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    static public float ShakeDuration = 0f;          // Time the Camera Shake effect will last
    static public float ShakeAmplitude = 0f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 1.0f;         // Cinemachine Noise Profile Parameter

    static private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    static public void Shake(float duration, float amplitude)
    {
        Debug.Log("ScreenShake || Shake:" + duration + "," + amplitude);
        ShakeDuration = duration;
        ShakeAmplitude = amplitude;
        ShakeElapsedTime = ShakeDuration;
    }
    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

}
