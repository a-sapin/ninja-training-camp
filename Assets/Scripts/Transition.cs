using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private GameObject shurikenTransition;
    public float TransitionTime { get; private set; }

    private void Start()
    {
        TransitionTime = 1.2f;
        shurikenTransition.SetActive(false);
    }
    public void TransitToScene(string sceneName)
    {
        IEnumerator loadScene = LoadScene(sceneName, TransitionTime);
        StartCoroutine(loadScene);
    }
    public void TransitToCanvas(GameObject newCanvas,GameObject oldCanvas)
    {
        IEnumerator loadCanvas = LoadCanvas(newCanvas, oldCanvas, TransitionTime);
        StartCoroutine(loadCanvas);
    }
    IEnumerator LoadCanvas(GameObject newCanvas, GameObject oldCanvas,float time)
    {
        shurikenTransition.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        
        oldCanvas.SetActive(false);
        newCanvas.SetActive(true);
        shurikenTransition.SetActive(false);
    }
    IEnumerator LoadScene(string sceneName,float time)
    {
        shurikenTransition.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        if(sceneName !=null)SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

}
