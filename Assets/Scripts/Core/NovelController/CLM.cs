using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLM : MonoBehaviour
{
    public static LINE Interpret(string rawline)
    {
        return new LINE(rawline);
    }

    public class LINE
    {

        public string speaker;

        public List<SEGMENT> segments = new List<SEGMENT>();
        public List<string> actions = new List<string>();

        public string LastSegmentwholeDialog = "";
        public LINE(string rawline)
        {
            string[] dialogAndActions = rawline.Split('"');
            char actionSplitter = ' ';
            string[] actionsArr = dialogAndActions.Length == 3 ? dialogAndActions[2].Split(actionSplitter) : dialogAndActions[0].Split(actionSplitter);

            if (dialogAndActions.Length == 3)
            {
                speaker = dialogAndActions[0] == "" ? NovelController.instance.cachedLastSpeaker : dialogAndActions[0];

                if (speaker[speaker.Length - 1] == ' ')
                {
                    speaker = speaker.Remove(speaker.Length - 1);
                }

                NovelController.instance.cachedLastSpeaker = speaker;

                SegmentDialog(dialogAndActions[1]);

            }
            for (int i = 0; i < actionsArr.Length; i++)
            {
                actions.Add(actionsArr[i]);
            }
        }

        void SegmentDialog(string dialog)
        {
            segments.Clear();
            string[] parts = dialog.Split('{', '}');

            for (int i = 0; i < parts.Length; i++)
            {
                SEGMENT segment = new SEGMENT();
                bool isOdd = i % 2 != 0;

                if (isOdd)
                {
                    string[] commandData = parts[i].Split(' ');
                    switch (commandData[0])
                    {
                        case "c":
                            segment.trigger = SEGMENT.TRIGGER.waitClickClear;
                            break;
                        case "a":
                            segment.trigger = SEGMENT.TRIGGER.waitClick;
                            segment.pretext = segments.Count > 0 ? segments[segments.Count - 1].dialog : "";
                            break;
                        case "w":
                            segment.trigger = SEGMENT.TRIGGER.autoDelay;
                            segment.autoDelay = float.Parse(commandData[1], System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "wa":
                            segment.trigger = SEGMENT.TRIGGER.autoDelay;
                            segment.autoDelay = float.Parse(commandData[1], System.Globalization.CultureInfo.InvariantCulture);
                            segment.pretext = segments.Count > 0 ? segments[segments.Count - 1].dialog : "";
                            break;
                    }
                    i++;
                }
                segment.dialog = parts[i];
                segment.line = this;
                segments.Add(segment);
            }
        }

        public class SEGMENT
        {
            public LINE line;
            public string dialog = "";
            public string pretext = "";
            public enum TRIGGER { waitClick, waitClickClear, autoDelay };
            public TRIGGER trigger = TRIGGER.waitClickClear;

            public float autoDelay = 0;

            public void Run()
            {
                if (running != null)
                {
                    NovelController.instance.StopCoroutine(running);
                }
                running = NovelController.instance.StartCoroutine(Running());
            }
            public bool isRunning { get { return running != null; } }
            Coroutine running = null;
            public TextArchitect architect = null;
            List<string> allCurrentlyExecutedEvents = new List<string>();

            IEnumerator Running()
            {
                TagManager.Inject(ref dialog, false);

                string[] parts = dialog.Split('[', ']');

                for (int i = 0; i < parts.Length; i++)
                {
                    bool isOdd = i % 2 != 0;
                    if (isOdd)
                    {
                        DialogEvents.HandleEvent(parts[i], this);
                        allCurrentlyExecutedEvents.Add(parts[i]);
                        i++;
                    }

                    DialogSystem.instance.OpenAllRequirementsForDialogueSystemVisibility(true);
                    string targDialog = parts[i];
                    if (line.speaker[0] == '*')
                    {
                        CharacterManager.instance.character.SetAnimation(CharacterManager.instance.character.animationSpeech, false);
                    }
                    else
                    {
                        CharacterManager.instance.character.SetAnimation(CharacterManager.instance.character.animationNormal, true);
                    }
                    DialogSystem.instance.Say(targDialog, line.speaker.Replace("*", ""), i > 0 ? true : pretext != "");
                    architect = DialogSystem.instance.textArchitect;

                    while (architect.isConstructing)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
                running = null;
            }

            public void ForceFinish()
            {
                if (running != null)
                {
                    NovelController.instance.StopCoroutine(running);
                }
                running = null;
                if (architect != null)
                {
                    architect.ForceFinish();

                    if (pretext == "")
                    {
                        line.LastSegmentwholeDialog = "";
                    }

                    string wholeDialog = "";
                    string[] parts = dialog.Split('[', ']');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        bool isOdd = i % 2 != 0;
                        if (isOdd)
                        {
                            string e = parts[i];
                            if (allCurrentlyExecutedEvents.Contains(e))
                            {
                                allCurrentlyExecutedEvents.Remove(e);
                            }
                            else
                            {
                                DialogEvents.HandleEvent(e, this);
                            }
                            i++;
                        }
                        wholeDialog += parts[i];
                    }

                    architect.ShowText(wholeDialog);
                }
            }
        }
    }
}
