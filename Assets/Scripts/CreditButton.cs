using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{

    float transitionTime = 0.43f;

    int sceneID;

    // Start is called before the first frame update
    public void AccessMainMenu()
    {
        sceneID = 1;
        StartCoroutine(LoadLevel(sceneID));
    }

    IEnumerator LoadLevel(int levelID)
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelID);
    }
}
