using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVisualNovel : MonoBehaviour
{

    public static LoadVisualNovel instance;

    public string ChapterToLoad;

    public int currentChapter;

    public bool loadSave;

    void Awake(){
        instance = this;
    }

    public void LoadChapter(string chapter,int num,bool _loadSave){
        ChapterToLoad = chapter;
        loadSave = _loadSave;
        if(num != -1){
            currentChapter = num;
        }
        SceneManager.LoadScene("VN");
    }

}
