using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private string level;
    private LevelInformations info;
    private ScenesTransitionManager transition;

    void Start()
    {
        if(level != "Level1")
            gameObject.SetActive(PlayerPrefs.GetInt(level + "Finished", 0) > 0);

        info = FindObjectOfType<LevelInformations>();
        transition = FindObjectOfType<ScenesTransitionManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        transition.GoToLevel(level);
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
