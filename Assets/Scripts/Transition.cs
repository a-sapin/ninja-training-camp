using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [Header("Optionnal particle system")] [SerializeField]
    private new ParticleSystem particleSystem;

    private void Start()
    {
        Time.timeScale = 0;
        transform.localPosition = new Vector3(-(Screen.width + 500), 0, 0);
        StartCoroutine(FadeOut(true));
    }

    IEnumerator AnimateTransition()
    {
        Time.timeScale = 0;
        Vector3 currentPos = transform.localPosition;
        float t = 0f;
        float posDiff = -(Screen.width + 500)/50;
        while (t < 1)
        {
            t += 0.02f;
            transform.localPosition += new Vector3(posDiff, 0, 0); //Vector3.Lerp(currentPos, new Vector3(-(Screen.width + 500), 0, 0), t);
            Debug.Log(transform.localPosition);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        
    }
    
    IEnumerator FadeOut(Boolean timescale)
    {
        float t = 0f;
        while (t < 1)
        {
            t += 0.02f;
            transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1 - t);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        
        transform.localPosition = new Vector3(0, 0, 0);
        transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
        if (particleSystem != null) particleSystem.gameObject.SetActive(false);
        if (timescale) Time.timeScale = 1;
    }
}
