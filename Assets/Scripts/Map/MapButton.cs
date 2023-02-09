using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public string locationID;
    public string nextChapter;
    [SerializeField] private string requirementName;
    [SerializeField] private int requirementValue;

    public bool canBeSeen
    {
        get
        {
            return int.Parse(GameItems.instance.GetItem(requirementName)) >= requirementValue;
        }
    }

    public void OnClick()
    {
        Map.instance.GoToChapter(nextChapter);
    }

    public void OnMouseEnter()
    {
        Map.instance.ShowInfo(true, locationID);
    }

    public void OnMouseExit()
    {
        Map.instance.ShowInfo(false, "");
    }
}
