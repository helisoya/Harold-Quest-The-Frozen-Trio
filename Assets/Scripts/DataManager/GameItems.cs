using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour
{
    public static GameItems instance;

    public GAMEITEMSSAVE save;

    void Awake()
    {
        instance = this;

        for(int i =0;i<save.items.Count;i++){
            save.items[i].defaultValue = save.items[i].value;
        }
    }

    int GetIndexOfItem(string key){
        for(int i =0;i<save.items.Count;i++){
            if(save.items[i].name == key){
                return i;
            }
        }
        return 0;
    }

    public void EditItem(string key, string _value){
        save.items[GetIndexOfItem(key)].value = _value;
    }

    public string GetItem(string key){
        return save.items[GetIndexOfItem(key)].value;
    }

    public void ResetAllItems(){
        for(int i =0;i<save.items.Count;i++){
            save.items[i].value = save.items[i].defaultValue;
        }
    }


    public void LoadFile(){
        save = FileManager.LoadJSON<GAMEITEMSSAVE>(FileManager.savPath + "userData/gameFiles/"+LoadVisualNovel.instance.currentChapter.ToString()+"i.txt");
    }

    public void SaveFile(){
        FileManager.SaveJSON(FileManager.savPath + "userData/gameFiles/"+LoadVisualNovel.instance.currentChapter.ToString()+"i.txt",save);
    }

}
