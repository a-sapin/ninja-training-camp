using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerLevier : MonoBehaviour
{
    public GameObject associatedGo;
    [SerializeField] private TypeLevier typeLevier;

    [SerializeField] private float timer = 1;
    [SerializeField] private float distanceToMove = 1;
    [SerializeField] private bool doOnce = false;

    private bool isActivated;
    private Animator animator;
    private static readonly int LevierToggled = Animator.StringToHash("LevierToggled");
    public Vector3 originalLiftPosition;

    private enum TypeLevier
    {
        RaiseWall,
        MoveRight,
        MoveLeft,
        MoveDown
    }

    private void Start()
    {
        isActivated = false;
        animator = GetComponent<Animator>();
        originalLiftPosition = associatedGo.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            

            if (isActivated) return;
            switch (typeLevier)
            {
                case TypeLevier.RaiseWall:
                    Debug.Log("TriggerLevier || RaiseWall");
                    
                    StartCoroutine(RaiseWall(timer,Vector3.up));
                    break;
                case TypeLevier.MoveRight:
                    break;
                case TypeLevier.MoveLeft:
                    break;
                case TypeLevier.MoveDown:
                    StartCoroutine(RaiseWall(timer, Vector3.down));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if(doOnce)
                isActivated = true;
            animator.SetBool(LevierToggled, isActivated);
        }
    }

    private IEnumerator RaiseWall(float delayTime, Vector3 directionVector)
    {
        var startTime = Time.time;
        while (Time.time - startTime <= delayTime)
        {
            var position = associatedGo.transform.position;
            position = Vector3.Lerp(position,
                position + directionVector * Time.deltaTime * distanceToMove,
                Time.time - startTime);
            associatedGo.transform.position = position;
            yield return null;
        }
    }

    public void ResetLevier()
    {
        associatedGo.transform.position = originalLiftPosition;
        isActivated = false;
        animator.SetBool(LevierToggled, isActivated);
    }
}