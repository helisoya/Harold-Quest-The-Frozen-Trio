using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GAMEFILE
{
    public string chapterName;
    public int chapterProgress = 0;

    public string cachedLastSpeaker = "";

    public string currentTextSystemDisplayText = "";
    public string currentTextSystemSpeakerDisplayText = "";

    public CHARACTERDATA characterInScene = null;


    public string background = null;

    public string cinematic = null;

    public string foreground = null;

    public AudioClip music = null;

    public int progressFixed = -1;

    public string playerName;

    public GAMEFILE()
    {
        this.chapterName = "test2";
        this.chapterProgress = 0;
        this.cachedLastSpeaker = "";
        this.characterInScene = null;
        this.background = null;
        this.cinematic = null;
        this.foreground = null;
        this.music = null;
        this.progressFixed = -1;
        playerName = "Harold";
    }

    [System.Serializable]
    public class CHARACTERDATA
    {
        public string AnimationSetName = "";
        public Color color;
        public Vector2 position = Vector2.zero;

        public bool preserveAspect = false;

        public CHARACTERDATA(Character character)
        {
            this.AnimationSetName = character.AnimationSetName;
            this.position = character._targetPosition;
            this.color = character.GetColor();
            this.preserveAspect = character.preserveAspect;
        }
    }
}
