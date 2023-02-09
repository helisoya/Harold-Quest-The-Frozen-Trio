using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public Slider sliderBGM;
    public Slider sliderVOICE;
    public Slider sliderSFX;

    private Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;


    void Start()
    {
        MASTERSAVE save = MasterItems.instance.save;
        sliderBGM.value = save.valBGM;
        sliderVOICE.value = save.valVOICE;
        sliderSFX.value = save.valSFX;
        fullscreenToggle.isOn = save.fullscreen;


        resolutions = Screen.resolutions;

        List<string> list = new List<string>();

        int currentRes = 0;

        Resolution res;
        Resolution reference = Screen.currentResolution;
        for (int i = 0; i < resolutions.Length; i++)
        {
            res = resolutions[i];
            list.Add(res.width + "x" + res.height);
            if (currentRes == 0 && res.width == reference.width && res.height == reference.height && res.refreshRate == reference.refreshRate)
            {
                currentRes = i;
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(list);
        resolutionDropdown.SetValueWithoutNotify(currentRes);
    }

    public void SetBGM()
    {
        MasterItems.instance.SetVolumeBGM(sliderBGM.value);
    }

    public void SetVoice()
    {
        MasterItems.instance.SetVolumeVOICE(sliderVOICE.value);
    }

    public void SetSFX()
    {
        MasterItems.instance.SetVolumeSFX(sliderSFX.value);
    }


    public void ChangeResolution(int value)
    {
        Resolution newRes = resolutions[value];
        MasterItems.instance.SetResolution(newRes.width, newRes.height, newRes.refreshRate);
    }

    public void ChangeFullScreen(bool value)
    {
        MasterItems.instance.SetFullscreen(value);
    }


    public void CloseMenu()
    {
        MasterItems.instance.CloseSettingsMenu();
    }


    public void ChangeLanguage(string newVal)
    {
        MasterItems.instance.ChangeLanguage(newVal);
    }

}
