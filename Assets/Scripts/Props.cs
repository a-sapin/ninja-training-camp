using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Props : MonoBehaviour
{
    [Header("Double jump")] [SerializeField]
    private GameObject jumpSmoke;
    [SerializeField] GameObject doubleJumpSmoke;

    [Header("Water splash")] [SerializeField]
    private GameObject waterSplashEnter;
    [SerializeField] GameObject waterSplashExit;

    public void CreateJumpSmoke()
    {
        Instantiate(jumpSmoke, gameObject.transform.position, Quaternion.identity);
    }
    public void CreateDoubleJumpSmoke()
    {
        Instantiate(doubleJumpSmoke, gameObject.transform.position, Quaternion.identity);
    }
    
    public void CreateWaterSplashEnter(Vector2 position, float speed)
    {
        GameObject splash = Instantiate(waterSplashEnter, new Vector3(position.x, position.y, -9), Quaternion.identity);
        splash.transform.localScale = new Vector3(3.5f, speed/2, 1);
    }
    public void CreateWaterSplashExit(Vector2 position, float speed)
    {
        GameObject splash = Instantiate(waterSplashExit, new Vector3(position.x, position.y-0.2f, -9), Quaternion.identity);
        splash.transform.localScale = new Vector3(3.5f, speed/2, 1);
    }
}
