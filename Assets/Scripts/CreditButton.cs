using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{

    float transitionTime = 0.43f;

    int sceneID;
    [SerializeField] private GameObject transition;

    // Start is called before the first frame update
    public void AccessMainMenu()
    {
        sceneID = 1;
        StartCoroutine(LoadLevel(sceneID));
    }

    IEnumerator LoadLevel(int levelID)
    {
        yield return new WaitForSecondsRealtime(transitionTime);
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(levelID, LoadSceneMode.Single);
    }
}
