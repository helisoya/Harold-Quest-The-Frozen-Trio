using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icons : MonoBehaviour
{
    public void Save()
    {
        NovelController.instance.SaveGameFile();
    }

    public void Load()
    {
        if (System.IO.File.Exists(FileManager.savPath + "userData/gameFiles/" + NovelController.instance.activeGameFileNumber.ToString() + ".txt"))
            NovelController.instance.LoadGameFile(NovelController.instance.activeGameFileNumber);
    }

    public void QuitToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettings()
    {
        MasterItems.instance.OpenSettingsMenu();
    }
}
