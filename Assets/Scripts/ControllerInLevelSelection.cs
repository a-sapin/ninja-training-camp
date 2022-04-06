using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerInLevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject[] arrowPos;
    [SerializeField] private Button[] linkedButton;
    private LevelInformations info;
    private int currentPos;

    private void Start()
    {
        StartCoroutine(nameof(ArrowMove));
        info = FindObjectOfType<LevelInformations>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Jump") > 0) linkedButton[currentPos].onClick.Invoke();
    }

    private IEnumerator ArrowMove()
    {
        while (true)
        {
            if (Input.GetAxisRaw("Vertical") > 0.3 || Input.GetAxisRaw("Horizontal") > 0.3)
            {
                if (currentPos == 0 ||
                    (currentPos == 1 && PlayerPrefs.GetInt("Level1Finished", 0) > 0))
                {
                    if (currentPos > 0) info.CloseLevelInfo();
                    currentPos++;
                    try
                    {
                        if (currentPos > 0) info.OpenLevelInfo("Level" + currentPos);
                    }
                    catch
                    {
                        Debug.Log($"ControllerInLevelSelection || Cannot open Level{currentPos}");
                    }

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
                    try
                    {
                        if (currentPos > 0) info.OpenLevelInfo("Level" + currentPos);
                    }
                    catch
                    {
                    }

                    arrow.transform.position = arrowPos[currentPos].transform.position;
                }

                yield return new WaitForSecondsRealtime(0.2f);
            }

            yield return null;
        }
    }
}