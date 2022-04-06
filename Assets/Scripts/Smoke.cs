using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] GameObject jumpSmoke, doubleJumpSmoke;

    public void CreateJumpSmoke()
    {
        Instantiate(jumpSmoke, gameObject.transform.position, gameObject.transform.rotation);
    }
    public void CreateDoubleJumpSmoke()
    {
        Instantiate(doubleJumpSmoke, gameObject.transform.position-new Vector3(0,0,0), gameObject.transform.rotation);
    }
}
