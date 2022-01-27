using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private string level;
    private LevelInformations info;

    void Start()
    {
        if(level != "Level1")
            gameObject.SetActive(PlayerPrefs.GetInt(level + "Finished", 0) > 0);

        info = FindObjectOfType<LevelInformations>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        info.OpenLevelInfo(level);
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info.CloseLevelInfo();
    }
}
