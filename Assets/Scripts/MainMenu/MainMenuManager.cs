using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    private int currentChapter = 0;
    public GameObject buttonRoot;

    public Button button_continue;

    public GameObject preMenu;

    public TMP_InputField nameField;
    public GameObject nameFieldRoot;

    void Start()
    {

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySong(null);
        }

    }


    public void OpenSettingsMenu()
    {
        MasterItems.instance.OpenSettingsMenu();
    }

    public void ClickToShowMenu()
    {
        preMenu.SetActive(false);
        LoadChapter(0);
    }


    public void OpenNewGame()
    {
        GameItems.instance.ResetAllItems();
        nameFieldRoot.SetActive(true);
        buttonRoot.SetActive(false);
    }

    public void LoadGame()
    {
        GameItems.instance.ResetAllItems();
        LoadVisualNovel.instance.LoadChapter("Intro_A", 0, true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadChapter(int x)
    {
        currentChapter = x;

        buttonRoot.SetActive(true);


        if (System.IO.File.Exists(FileManager.savPath + "userData/gameFiles/" + x.ToString() + ".txt"))
        {
            button_continue.gameObject.SetActive(true);
        }
    }


    public void AcceptPlayerName()
    {
        if (nameField.text.Replace(" ", "").Replace("\t", "") == "") return;
        GameItems.instance.ResetAllItems();
        GameItems.instance.EditItem("playerName", nameField.text);
        LoadVisualNovel.instance.LoadChapter("Intro_A", 0, false);
    }

    public void ChangeLocal(string newLocal)
    {
        MasterItems.instance.ChangeLanguage(newLocal);
    }

}