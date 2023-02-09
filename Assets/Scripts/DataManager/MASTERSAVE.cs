using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MASTERSAVE
{

    public float valBGM = 1;
    public float valSFX = 1;
    public float valVOICE = 1;
    public int resolutionWidth;
    public int resolutionHeight;
    public int refreshRate;
    public bool fullscreen;

    public MASTERSAVE()
    {
        valBGM = 0.2f;
        valSFX = 0.2f;
        valVOICE = 0.3f;
        fullscreen = true;
    }
}
