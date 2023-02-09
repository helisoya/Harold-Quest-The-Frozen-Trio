using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterItems : MonoBehaviour
{
    public static MasterItems instance = null;

    public MASTERSAVE save;

    public GameObject pausePrefab;

    public AudioMixer mixer;

    bool paused = false;

    private GameObject instancePause = null;

    void Awake()
    {

        if (MasterItems.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Locals.Init();

        instance = this;
        if (System.IO.File.Exists(FileManager.savPath + "userData/gameFiles/master.txt"))
        {
            save = FileManager.LoadJSON<MASTERSAVE>(FileManager.savPath + "userData/gameFiles/master.txt");
        }
        else
        {
            save = new MASTERSAVE();
            save.refreshRate = Screen.currentResolution.refreshRate;
            save.resolutionHeight = Screen.currentResolution.height;
            save.resolutionWidth = Screen.currentResolution.width;
            SaveFile();
        }

    }

    public void ChangeLanguage(string newLocal)
    {
        Locals.ChangeLanguage(newLocal);
        LocalizedText[] texts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText text in texts)
        {
            text.ReloadText();
        }
        if (DialogSystem.instance != null) DialogSystem.instance.UpdateTextLanguage();
    }



    public void OpenSettingsMenu()
    {
        paused = true;
        Time.timeScale = 0;
        if (DialogSystem.instance != null) DialogSystem.instance.StopBeforeOpeningSettings();
        if (instancePause != null)
        {
            Destroy(instancePause);
        }
        instancePause = Instantiate(pausePrefab, GameObject.Find("Canvas").transform);
    }

    public void CloseSettingsMenu()
    {
        paused = false;
        Time.timeScale = 1;
        if (instancePause != null)
        {
            Destroy(instancePause);
        }
        instancePause = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                CloseSettingsMenu();
            }
            else
            {
                OpenSettingsMenu();
            }
        }
    }


    void SaveFile()
    {
        FileManager.SaveJSON(FileManager.savPath + "userData/gameFiles/master.txt", save);
    }


    public void SetVolumeBGM(float sliderValue)
    {
        save.valBGM = sliderValue;
        SaveFile();
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVolumeVOICE(float sliderValue)
    {
        save.valVOICE = sliderValue;
        SaveFile();
        mixer.SetFloat("Voice", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVolumeSFX(float sliderValue)
    {
        save.valSFX = sliderValue;
        SaveFile();
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }


    public void SetFullscreen(bool value)
    {
        Screen.SetResolution(save.resolutionWidth, save.resolutionHeight, value, save.refreshRate);
        save.fullscreen = value;
        SaveFile();
    }

    public void SetResolution(int width, int height, int refreshRate)
    {
        Screen.SetResolution(width, height, save.fullscreen, refreshRate);
        save.refreshRate = refreshRate;
        save.resolutionHeight = height;
        save.resolutionWidth = width;
        SaveFile();
    }
}
