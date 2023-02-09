using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Starter : MonoBehaviour
{
    public static MG_Starter instance;

    public string WhatToLoadAfter;

    public bool canStart = false;

    public GameObject HelpToHide;

    void Awake()
    {
        instance = this;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySong(null);
        }

    }

    public void LoadNext()
    {
        LoadVisualNovel.instance.LoadChapter(WhatToLoadAfter, 0, false);
    }

    public void StartMiniGame()
    {
        HelpToHide.SetActive(false);
        canStart = true;
    }
}
