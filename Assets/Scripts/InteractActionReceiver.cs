using UnityEngine;

public class InteractActionReceiver : MonoBehaviour
{
    public MonoBehaviour targetScript;
    public string functionName;

    public void Activate()
    {
        targetScript.Invoke(functionName, 0.0f);
    }
}
