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
        StartCoroutine(FadeOut());
    }

    IEnumerator AnimateTransition()
    {
        Time.timeScale = 0;
        Vector3 currentPos = transform.localPosition;
        float t = 0f;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / 5;
            transform.localPosition = Vector3.Lerp(currentPos, new Vector3(-(Screen.width + 500), 0, 0), t);;
            yield return null;
        }
    }
    
    IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < 0.8)
        {
            t += Time.fixedDeltaTime / 5;
            transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1 - t);
            yield return null;
        }
        
        transform.localPosition = new Vector3(0, 0, 0);
        transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
        if (particleSystem != null) particleSystem.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
