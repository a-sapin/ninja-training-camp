using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInMenu : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject[] arrowPos;
    [SerializeField] Button[] linkedButton;
    private IEnumerator move;
    int currentPos = 0;

    public AudioSource buttonArrow;
    private bool isPressed = false;

    private void Start()
    {
        StartMove();
    }
    void Update()
    {
        if (Input.GetAxisRaw("Jump") > 0 && !isPressed)
        {
            isPressed = true;
            linkedButton[currentPos].onClick.Invoke();
        }
    }
    public void StartMove()
    {
        if (move != null) StopCoroutine(move);
        move = ArrowMove();
        StartCoroutine(move);
    }
    IEnumerator ArrowMove()
    {
        while (true)
        {
            if (Input.GetAxisRaw("Vertical") > 0.3)
            {
                buttonArrow.Play();
                currentPos = (currentPos - 1);
                currentPos = (currentPos >= 0 ? currentPos : arrowPos.Length - 1);
                arrow.transform.position = arrowPos[currentPos].transform.position;
                
                yield return new WaitForSecondsRealtime(0.2f);
            }
            else if(Input.GetAxisRaw("Vertical") < -0.3)
            {
                buttonArrow.Play();
                currentPos = (currentPos + 1) % arrowPos.Length;
                Debug.Log(currentPos);
                arrow.transform.position = arrowPos[currentPos].transform.position;
      
                yield return new WaitForSecondsRealtime(0.2f);

            }
            yield return null;
        }
    }
}
