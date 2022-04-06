using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightGlow : MonoBehaviour
{
    [SerializeField] private float minIntensity, maxIntensity;
    [SerializeField] private float minRadius, maxRadius;
    [SerializeField] private float lightLoopSpeed;
    new Light2D light;
 
    void Start()
    {
        light = GetComponent<Light2D>();
        StartCoroutine(LightRoutine());
    }
    IEnumerator LightRoutine()
    {
        light.pointLightOuterRadius = minRadius;
        light.intensity = minIntensity;
        float intensityStep = (maxIntensity - minIntensity) / 30;
        float radiusStep = (maxRadius - minRadius) / 30;
        bool isGlowing = true;
        while (true)
        {
            for(int i = 0; i < 30; i++)
            {
                light.intensity += isGlowing?intensityStep:-intensityStep;
                light.pointLightOuterRadius += isGlowing?radiusStep:-radiusStep;
                yield return new WaitForSeconds(lightLoopSpeed / 30f);
            }
            isGlowing = !isGlowing;
        }
    }
}
