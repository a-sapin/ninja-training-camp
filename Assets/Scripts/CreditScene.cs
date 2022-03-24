using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : MonoBehaviour
{
    public ScenesTransitionManager scenesTransition;
    public float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
