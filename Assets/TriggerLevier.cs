using System;
using System.Collections;
using UnityEngine;

public class TriggerLevier : MonoBehaviour
{
    [SerializeField] private GameObject associatedGo;
    [SerializeField] private TypeLevier typeLevier;

    [SerializeField] private float timer = 1;
    [SerializeField] private float distanceToMove = 1;
    [SerializeField] private bool doOnce = false;

    private bool isActivated;
    private Animator animator;
    private static readonly int LevierToggled = Animator.StringToHash("LevierToggled");

    private enum TypeLevier
    {
        RaiseWall,
        MoveRight,
        MoveLeft
    }

    private void Start()
    {
        isActivated = false;
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            

            if (isActivated) return;
            switch (typeLevier)
            {
                case TypeLevier.RaiseWall:
                    Debug.Log("TiggerLevier || RaiseWall");
                    
                    StartCoroutine(RaiseWall(timer));
                    break;
                case TypeLevier.MoveRight:
                    break;
                case TypeLevier.MoveLeft:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if(doOnce)
                isActivated = true;
            animator.SetBool(LevierToggled, isActivated);
        }
    }

    private IEnumerator RaiseWall(float delayTime)
    {
        var startTime = Time.time;
        while (Time.time - startTime <= delayTime)
        {
            var position = associatedGo.transform.position;
            position = Vector3.Lerp(position,
                position + associatedGo.transform.up * Time.deltaTime * distanceToMove,
                Time.time - startTime);
            associatedGo.transform.position = position;
            yield return null;
        }
    }
}