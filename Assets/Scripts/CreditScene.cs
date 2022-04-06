using UnityEngine;

public class CreditScene : MonoBehaviour
{
    public ScenesTransitionManager scenesTransition;
    public float timer;

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
