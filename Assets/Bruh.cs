using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruh : MonoBehaviour
{
    VFXManager vfx;
    private void Start()
    {
        vfx = FindObjectOfType<VFXManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            vfx.isBruh = true;
        }
    }
    private void OnDestroy()
    {
        vfx.isBruh = false;
    }
}
