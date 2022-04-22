using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : MonoBehaviour
{
    public ScenesTransitionManager scenesTransition;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("A key or mouse click has been detected");
            scenesTransition.GoToMenu();
        }
    }
}
