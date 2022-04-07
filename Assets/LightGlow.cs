using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightGlow : MonoBehaviour
{
    [SerializeField] private float minIntensity, maxIntensity;
    [SerializeField] private float minRadius, maxRadius;
    [SerializeField] private float lightLoopSpeed;
    private Light2D _light;
 
    void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(LightRoutine());
    }
    IEnumerator LightRoutine()
    {
        _light.pointLightOuterRadius = minRadius;
        _light.intensity = minIntensity;
        float intensityStep = (maxIntensity - minIntensity) / 30;
        float radiusStep = (maxRadius - minRadius) / 30;
        bool isGlowing = true;
        while (true)
        {
            for(int i = 0; i < 30; i++)
            {
                _light.intensity += isGlowing?intensityStep:-intensityStep;
                _light.pointLightOuterRadius += isGlowing?radiusStep:-radiusStep;
                yield return new WaitForSeconds(lightLoopSpeed / 30f);
            }
            isGlowing = !isGlowing;
        }
    }
}
