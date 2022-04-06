using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
