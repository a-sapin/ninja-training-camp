using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    [SerializeField]private float limXNeg;//Limite vertical vers le bas de la camera
    Camera camera;
    void Update()
    {
        if(camera.transform.position.y < limXNeg)
        {
            camera.transform.position = new Vector3(
                camera.transform.position.x,
                limXNeg,
                camera.transform.position.z);
        }
    }
}
