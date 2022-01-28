using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInLevelSelection : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject[] arrowPos;
    [SerializeField] Button[] linkedButton;
    LevelInformations info;
    int currentPos = 0;
    private void Start()
    {
        StartCoroutine(nameof(ArrowMove));
        info = FindObjectOfType<LevelInformations>();
    }
    void Update()
    {
        if (Input.GetAxisRaw("Jump") > 0)
        {
            linkedButton[currentPos].onClick.Invoke();
        }
    }
    IEnumerator ArrowMove()
    {
        while (true)
        {
            if ((Input.GetAxisRaw("Vertical") > 0.3 || Input.GetAxisRaw("Horizontal") > 0.3))
            {
                if(currentPos == 0||
                   (currentPos == 1 && PlayerPrefs.GetInt("Level1Finished", 0) > 0)
                   )
                {
                    if (currentPos > 0) info.CloseLevelInfo();
                    currentPos++;
                    try{if (currentPos > 0) info.OpenLevelInfo("Level" + currentPos);}catch{}
                    
                    arrow.transform.position = arrowPos[currentPos].transform.position;
                }
                yield return new WaitForSecondsRealtime(0.2f);
            }
            else if (Input.GetAxisRaw("Vertical") < -0.3 || Input.GetAxisRaw("Horizontal") < -0.3)
            {
                if (currentPos > 0)
                {
                    info.CloseLevelInfo();
                    currentPos--;
                    try { if (currentPos > 0) info.OpenLevelInfo("Level" + currentPos); } catch { }
                    arrow.transform.position = arrowPos[currentPos].transform.position;
                }
                yield return new WaitForSecondsRealtime(0.2f);

            }
            yield return null;
        }
    }
}
