using Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static float shakeDuration;          // Time the Camera Shake effect will last
    public static float shakeAmplitude;         // Cinemachine Noise Profile Parameter
    public float shakeFrequency = 1.0f;         // Cinemachine Noise Profile Parameter

    private static float _shakeElapsedTime;

    // Cinemachine Shake
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public static void Shake(float duration, float amplitude)
    {
        Debug.Log("Shake:" + duration + "," + amplitude);
        shakeDuration = duration;
        shakeAmplitude = amplitude;
        _shakeElapsedTime = shakeDuration;
    }
    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (virtualCamera != null)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the Cinemachine componet is not set, avoid update
        if (virtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (_shakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                // Update Shake Timer
                _shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                _shakeElapsedTime = 0f;
            }
        }
    }

}
