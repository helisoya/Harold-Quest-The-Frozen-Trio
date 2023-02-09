using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private MapButton[] buttons;

    [SerializeField] private GameObject root;

    public static Map instance;

    [SerializeField] private MapInfo cursor;

    [SerializeField] private GameObject questRoot;

    [SerializeField] private Transform questsParent;
    [SerializeField] private GameObject questPrefab;

    [SerializeField] private string[] questsId;

    public void ClickQuestButton()
    {
        if (questRoot.activeInHierarchy) CloseQuests();
        else OpenQuests();
    }

    public void CloseQuests()
    {
        questRoot.SetActive(false);
    }

    public void OpenQuests()
    {
        questRoot.SetActive(true);
        foreach (Transform child in questsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (string questId in questsId)
        {
            string value = GameItems.instance.GetItem(questId);
            if (value != "-1" && value != "100")
            {
                Instantiate(questPrefab, questsParent).GetComponent<QuestInfo>().Init(questId, value);
            }
        }
    }

    public void ShowInfo(bool show, string key)
    {
        cursor.UpdateInfo(show, key);
    }

    void Awake()
    {
        instance = this;
    }

    public void OpenMap(string currentPoint)
    {
        CloseQuests();
        root.SetActive(true);
        foreach (MapButton button in buttons)
        {
            button.gameObject.SetActive(button.canBeSeen);
        }
    }

    public void CloseMap()
    {
        root.SetActive(false);
    }

    public void GoToChapter(string filename)
    {
        cursor.UpdateInfo(false, "");
        CloseMap();
        NovelController.instance.LoadChapterFile(filename);
    }
}
