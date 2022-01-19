using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInMenu : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject[] arrowPos;
    [SerializeField] Button[] linkedButton;
    int currentPos = 0;
    private void Start()
    {
        StartCoroutine(nameof(ArrowMove));
    }
    void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            linkedButton[currentPos].onClick.Invoke();
        }
    }
    IEnumerator ArrowMove()
    {
        while (true)
        {
            if (Input.GetAxis("Vertical") > 0.3)
            {
                currentPos = (currentPos - 1);
                currentPos = (currentPos >= 0 ? currentPos : arrowPos.Length - 1);
                arrow.transform.position = arrowPos[currentPos].transform.position;
                yield return new WaitForSeconds(0.2f);
            }
            else if(Input.GetAxis("Vertical") < -0.3)
            {
                currentPos = (currentPos + 1) % arrowPos.Length;
                Debug.Log(currentPos);
                arrow.transform.position = arrowPos[currentPos].transform.position;
                yield return new WaitForSeconds(0.2f);

            }
            yield return null;
        }
    }
}
