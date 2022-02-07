using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevier : MonoBehaviour
{
    [SerializeField] private GameObject associatedGO;
    [SerializeField] private TypeLevier typeLevier;

    enum TypeLevier
    {
        RaiseWall
    };
   private void OnCollisionEnter2D(Collision2D col)
       {
           if (col.gameObject.CompareTag("Player"))
           {
               switch (typeLevier)
               {
                   case TypeLevier.RaiseWall:
                      associatedGO.transform.Translate(associatedGO.transform.up);
                      break;
               }
           }
       }
}
