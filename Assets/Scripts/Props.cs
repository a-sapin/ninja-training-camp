using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Props : MonoBehaviour
{
    [SerializeField] GameObject jumpSmoke, doubleJumpSmoke, waterSplashEnter, waterSplashExit;

    public void CreateJumpSmoke()
    {
        Instantiate(jumpSmoke, gameObject.transform.position, gameObject.transform.rotation);
    }
    public void CreateDoubleJumpSmoke()
    {
        Instantiate(doubleJumpSmoke, gameObject.transform.position-new Vector3(0,0,0), gameObject.transform.rotation);
    }
    
    public void CreateWaterSplashEnter()
    {
        Instantiate(waterSplashEnter, gameObject.transform.position, gameObject.transform.rotation);
    }
    public void CreateWaterSplashExit()
    {
        Instantiate(waterSplashExit, gameObject.transform.position+new Vector3(0,1,0), gameObject.transform.rotation);
    }
}
