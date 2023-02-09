using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelController : MonoBehaviour
{

    public static NovelController instance;
    List<string> data = new List<string>();

    GAMEFILE activeGameFile = null;

    public int activeGameFileNumber = 0;

    string activeChapterFile = "";

    void Start()
    {
        instance = this;
        activeGameFileNumber = LoadVisualNovel.instance.currentChapter;
        if (LoadVisualNovel.instance.loadSave)
        {
            LoadVisualNovel.instance.loadSave = false;
            LoadGameFile(activeGameFileNumber);
        }
        else
        {
            LoadChapterFile(LoadVisualNovel.instance.ChapterToLoad);
        }

    }

    public void LoadGameFile(int gameFileNumber)
    {
        string filePath = FileManager.savPath + "userData/gameFiles/" + gameFileNumber.ToString() + ".txt";
        activeGameFileNumber = gameFileNumber;
        activeGameFile = FileManager.LoadJSON<GAMEFILE>(filePath);
        data = FileManager.ReadTextAsset(Resources.Load<TextAsset>($"Story/{activeGameFile.chapterName}"));
        cachedLastSpeaker = activeGameFile.cachedLastSpeaker;
        activeChapterFile = activeGameFile.chapterName;
        DialogSystem.instance.Open(activeGameFile.currentTextSystemSpeakerDisplayText, activeGameFile.currentTextSystemDisplayText);


        GAMEFILE.CHARACTERDATA dat = activeGameFile.characterInScene;

        CharacterManager.instance.ChangeCharacter(dat.AnimationSetName, dat.preserveAspect);
        CharacterManager.instance.character.SetColor(dat.color);

        if (activeGameFile.background != null)
        {
            Texture2D tex = activeGameFile.background == "null" ? null : Resources.Load("Backgrounds/" + activeGameFile.background) as Texture2D;
            BCFC.instance.background.SetTexture(tex);
        }

        if (activeGameFile.cinematic != null)
        {
            Texture2D tex = activeGameFile.cinematic == "null" ? null : Resources.Load("Backgrounds/" + activeGameFile.cinematic) as Texture2D;
            BCFC.instance.cinematic.SetTexture(tex);
        }
        if (activeGameFile.foreground != null)
        {
            Texture2D tex = activeGameFile.foreground == "null" ? null : Resources.Load("Backgrounds/" + activeGameFile.foreground) as Texture2D;
            BCFC.instance.foreground.SetTexture(tex);
        }

        if (activeGameFile.music != null)
            AudioManager.instance.PlaySong(activeGameFile.music);

        if (handlingLine != null)
        {
            StopCoroutine(handlingChapterFile);
        }

        GameItems.instance.LoadFile();

        handlingChapterFile = StartCoroutine(HandlingChapterFile());
        if (activeGameFile.progressFixed != -1)
        {
            chapterProgress = activeGameFile.progressFixed;
            Next();
        }
        else
        {
            chapterProgress = activeGameFile.chapterProgress;
        }

    }

    public void SaveGameFile()
    {
        string filePath = FileManager.savPath + "userData/gameFiles/" + activeGameFileNumber.ToString() + ".txt";
        if (activeGameFile == null)
        {
            activeGameFile = new GAMEFILE();
        }
        activeGameFile.chapterName = activeChapterFile;
        activeGameFile.cachedLastSpeaker = cachedLastSpeaker;
        activeGameFile.chapterProgress = chapterProgress;
        activeGameFile.currentTextSystemDisplayText = DialogSystem.instance.speechText.text;
        activeGameFile.currentTextSystemSpeakerDisplayText = DialogSystem.instance.speakerNameText.text;
        activeGameFile.progressFixed = progressFixed;

        activeGameFile.characterInScene = new GAMEFILE.CHARACTERDATA(CharacterManager.instance.character);


        BCFC b = BCFC.instance;
        activeGameFile.background = b.background.activeImage != null ? b.background.activeImage.texture.name : null;
        activeGameFile.cinematic = b.cinematic.activeImage != null ? b.cinematic.activeImage.texture.name : null;
        activeGameFile.foreground = b.foreground.activeImage != null ? b.foreground.activeImage.texture.name : null;

        activeGameFile.music = AudioManager.activeSong != null ? AudioManager.activeSong.clip : null;

        FileManager.SaveJSON(filePath, activeGameFile);

        GameItems.instance.SaveFile();
    }

    public void LoadChapterFile(string filename)
    {
        activeChapterFile = filename;
        chapterProgress = 0;

        data = FileManager.ReadTextAsset(Resources.Load<TextAsset>($"Story/{filename}"));
        cachedLastSpeaker = "";

        if (handlingLine != null)
        {
            StopCoroutine(handlingChapterFile);
        }
        handlingChapterFile = StartCoroutine(HandlingChapterFile());

        Next();
    }

    bool _next = false;
    public void Next()
    {
        _next = true;
    }

    public bool isHandlingChapter { get { return handlingChapterFile != null; } }
    Coroutine handlingChapterFile = null;
    int chapterProgress = 0;

    int progressFixed = -1;


    IEnumerator HandlingChapterFile()
    {
        chapterProgress = 0;

        while (chapterProgress < data.Count)
        {
            if (_next)
            {
                string line = data[chapterProgress];

                if (line.StartsWith("choice"))
                {
                    progressFixed = chapterProgress;
                    yield return HandlingChoiceLine(line);
                    chapterProgress++;
                }
                else if (line.StartsWith("if"))
                {
                    print("hello");
                    yield return HandlingIf(line);
                }
                else
                {
                    progressFixed = -1;
                    Handleline(line);
                    chapterProgress++;
                    while (isHandlingLine)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    if (CLM.Interpret(line).segments.Count == 0)
                    {
                        yield return new WaitForEndOfFrame();
                        Next();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }

        handlingChapterFile = null;
    }


    IEnumerator HandlingIf(string line)
    {
        string[] split = line.Split('"');
        string key = split[1];
        string value = split[3];
        bool valueRequired = split[2] == "=";
        print(key + " - " + value);
        print(GameItems.instance.GetItem(key) + " == " + value);

        if ((GameItems.instance.GetItem(key) == value) == valueRequired)
        {
            chapterProgress++;
            string action = data[chapterProgress].Replace(" ", "");
            HandleAction(action);
        }
        else
        {
            chapterProgress += 2;
        }


        yield return new WaitForEndOfFrame();
    }


    IEnumerator HandlingChoiceLine(string line)
    {
        string title = line.Split('"')[1];
        List<string> choices = new List<string>();
        List<string> actions = new List<string>();

        bool gatheringChoices = true;
        while (gatheringChoices)
        {
            chapterProgress++;
            line = data[chapterProgress];
            if (line == "{")
            {
                continue;
            }
            line = line.Replace("\t", "");

            if (line != "}")
            {
                choices.Add(line.Split('"')[1]);
                actions.Add(data[chapterProgress + 1].Replace("\t", ""));
                chapterProgress++;
            }
            else
            {
                gatheringChoices = false;
            }
        }

        if (choices.Count > 0)
        {
            ChoiceScreen.Show(title, choices.ToArray());
            yield return new WaitForEndOfFrame();
            while (ChoiceScreen.isWaitingForChoiceToBeMade)
            {
                yield return new WaitForEndOfFrame();
            }
            string action = actions[ChoiceScreen.lastChoiceMade.index];
            Handleline(action);

            while (isHandlingLine)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void Handleline(string rawline)
    {
        CLM.LINE line = CLM.Interpret(rawline);

        StopHandlingLine();
        handlingLine = StartCoroutine(HandlingLine(line));
    }


    void StopHandlingLine()
    {
        if (isHandlingLine)
        {
            StopCoroutine(handlingLine);
        }
        handlingLine = null;
    }

    [HideInInspector]
    public string cachedLastSpeaker = "";

    public bool isHandlingLine { get { return handlingLine != null; } }
    Coroutine handlingLine = null;
    IEnumerator HandlingLine(CLM.LINE line)
    {
        _next = false;
        int lineProgress = 0;
        while (lineProgress < line.segments.Count)
        {
            _next = false;
            CLM.LINE.SEGMENT segment = line.segments[lineProgress];

            if (lineProgress > 0)
            {
                if (segment.trigger == CLM.LINE.SEGMENT.TRIGGER.autoDelay)
                {
                    for (float timer = segment.autoDelay; timer >= 0; timer -= Time.deltaTime)
                    {
                        yield return new WaitForEndOfFrame();
                        if (_next)
                            break;
                    }
                }
                else
                {
                    while (!_next)
                        yield return new WaitForEndOfFrame();
                }
            }
            _next = false;


            segment.Run();

            while (segment.isRunning)
            {
                yield return new WaitForEndOfFrame();

                if (_next)
                {

                    if (!segment.architect.skip)
                    {
                        segment.architect.skip = true;
                    }
                    else
                    {
                        segment.ForceFinish();
                    }
                    _next = false;
                }
            }

            lineProgress++;

            yield return new WaitForEndOfFrame();
        }


        for (int i = 0; i < line.actions.Count; i++)
        {
            HandleAction(line.actions[i]);
        }

        handlingLine = null;
    }


    public void HandleAction(string action)
    {
        string[] data = action.Split('(', ')');
        switch (data[0])
        {
            case "setBackground":
                Command_SetLayerImage(data[1], BCFC.instance.background);
                break;

            case "setCinematic":
                Command_SetLayerImage(data[1], BCFC.instance.cinematic);
                break;

            case "setForeground":
                Command_SetLayerImage(data[1], BCFC.instance.foreground);
                break;

            case "playSound":
                Command_PlaySound(data[1]);
                break;

            case "playMusic":
                Command_PlayMusic(data[1]);
                break;

            case "changeCharacter":
                Command_ChangeCharacter(data[1]);
                break;

            case "setPosition":
                Command_SetPosition(data[1]);
                break;

            case "moveCharacter":
                Command_MoveCharacter(data[1]);
                break;

            case "show":
                Command_FadeIn(data[1]);
                break;

            case "hide":
                Command_FadeOut(data[1]);
                break;

            case "transBackground":
                Command_TransLayer(BCFC.instance.background, data[1]);
                break;

            case "transCinematic":
                Command_TransLayer(BCFC.instance.cinematic, data[1]);
                break;

            case "transForeground":
                Command_TransLayer(BCFC.instance.foreground, data[1]);
                break;

            case "Load":
                LoadChapterFile(data[1]);
                break;

            case "MainMenu":
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                break;

            case "LoadScene":
                UnityEngine.SceneManagement.SceneManager.LoadScene(data[1]);
                break;

            case "voice":
                Command_PlayVoice(data[1]);
                break;

            case "Map":
                Command_Map(data[1]);
                break;

            case "variable":
                Command_Variable(data[1]);
                break;
        }


    }

    public void Command_Variable(string data)
    {
        string[] split = data.Split(",");
        GameItems.instance.EditItem(split[0], split[1]);
    }

    public void Command_Map(string currentPoint)
    {
        Map.instance.OpenMap(currentPoint);
    }


    void Command_SetLayerImage(string data, BCFC.LAYER layer)
    {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        Texture2D tex = texName == "null" ? null : Resources.Load("Backgrounds/" + texName) as Texture2D;
        float sped = 2f;
        bool smooth = false;

        if (data.Contains(","))
        {
            string[] parameters = data.Split(',');
            foreach (string p in parameters)
            {
                float fVal = 0;
                bool bVal = false;
                if (float.TryParse(p, out fVal))
                {
                    sped = fVal;
                    continue;
                }
                if (bool.TryParse(p, out bVal))
                {
                    smooth = bVal;
                    continue;
                }

            }
        }
        layer.TransitionToTexture(tex, sped, smooth);
    }

    void Command_PlaySound(string data)
    {
        AudioClip clip = Resources.Load("Audio/SFX/" + data) as AudioClip;
        if (clip != null)
        {
            AudioManager.instance.PlaySFX(clip);
        }

    }

    void Command_PlayMusic(string data)
    {
        AudioClip clip = Resources.Load("Audio/Music/" + data) as AudioClip;
        AudioManager.instance.PlaySong(clip);
    }

    void Command_PlayVoice(string data)
    {
        AudioClip clip = Resources.Load("Audio/Voice/" + data) as AudioClip;
        if (clip != null)
        {
            AudioManager.instance.PlayVoice(clip);
        }
    }

    void Command_SetPosition(string data)
    {
        string[] parameters = data.Split(',');
        float locationX = float.Parse(parameters[0], System.Globalization.CultureInfo.InvariantCulture);
        float locationY = float.Parse(parameters[1], System.Globalization.CultureInfo.InvariantCulture);

        CharacterManager.instance.character.SetPosition(new Vector2(locationX, locationY));
    }

    void Command_MoveCharacter(string data)
    {
        string[] parameters = data.Split(',');
        float locationX = float.Parse(parameters[0], System.Globalization.CultureInfo.InvariantCulture);
        float locationY = float.Parse(parameters[1], System.Globalization.CultureInfo.InvariantCulture);
        float speed = parameters.Length == 4 ? float.Parse(parameters[2], System.Globalization.CultureInfo.InvariantCulture) : 1f;
        bool smooth = parameters.Length == 5 ? bool.Parse(parameters[3]) : false;

        CharacterManager.instance.character.MoveTo(new Vector2(locationX, locationY), speed, smooth);
    }

    void Command_ChangeCharacter(string data)
    {
        string[] sliced = data.Split(',');
        CharacterManager.instance.ChangeCharacter(sliced[0], sliced.Length == 2 ? sliced[1] == "true" : false);
    }


    void Command_FadeIn(string data)
    {
        CharacterManager.instance.character.FadeIn();
    }

    void Command_FadeOut(string data)
    {
        CharacterManager.instance.character.FadeOut();
    }

    void Command_TransLayer(BCFC.LAYER layer, string data)
    {
        string[] parameters = data.Split(',');
        string texName = parameters[0];
        string transTexName = parameters[1];
        Texture2D tex = texName == "null" ? null : Resources.Load("Backgrounds/" + texName) as Texture2D;
        Texture2D transTex = Resources.Load("Transition/" + transTexName) as Texture2D;
        float speed = 1f;
        bool smooth = false;

        for (int i = 2; i < parameters.Length; i++)
        {
            string p = parameters[i];
            float fVal = 0;
            bool bVal = false;
            if (float.TryParse(p, out fVal))
            {
                speed = fVal;
                continue;
            }
            if (bool.TryParse(p, out bVal))
            {
                smooth = bVal;
                continue;
            }
        }
        TransitionMaster.TransistionLayer(layer, tex, transTex, speed, smooth);
    }
}
