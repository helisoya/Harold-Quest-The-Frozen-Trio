using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance;

    public ELEMENTS elements;

    private string currentSpeakerID;
    private string currentSpeechID;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Say something and show it on the speech box.
    /// </summary>
    public void Say(string speech, string speaker = "", bool additive = false)
    {
        StopSpeaking();

        if (additive)
        {
            speechText.text = targetSpeech;
        }
        speaking = StartCoroutine(Speaking(speech, additive, speaker));
    }


    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        if (textArchitect != null && textArchitect.isConstructing)
        {
            textArchitect.Stop();
        }
        speaking = null;
    }

    public bool isSpeaking { get { return speaking != null; } }
    [HideInInspector] public bool isWaitingForUserInput = false;

    public string targetSpeech = "";
    Coroutine speaking = null;
    public TextArchitect textArchitect = null;


    public void UpdateTextLanguage()
    {
        if (speechText == null) return;
        string speaker = new string(currentSpeakerID);
        TagManager.Inject(ref speaker);
        string speech = Locals.GetLocal(currentSpeechID);
        TagManager.Inject(ref speech, false);

        speechText.text = speech;
        speakerNameText.text = speaker;
        speechText.maxVisibleCharacters = speech.Length;
    }

    public void StopBeforeOpeningSettings()
    {
        speechText.maxVisibleCharacters = 200;
        StopSpeaking();
    }

    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        speechPanel.SetActive(true);

        currentSpeakerID = new string(speaker);
        currentSpeechID = new string(speech);
        TagManager.Inject(ref speaker);

        speech = Locals.GetLocal(speech);
        TagManager.Inject(ref speech, false);

        string additiveSpeech = additive ? speechText.text : "";
        targetSpeech = additiveSpeech + speech;

        textArchitect = new TextArchitect(speechText.GetComponent<TextMeshProUGUI>(), speech, additiveSpeech);

        speakerNameText.text = DetermineSpeaker(speaker);
        speakerNameText.transform.parent.gameObject.SetActive(speakerNameText.text != "" && speakerNameText.text != "narrator");

        isWaitingForUserInput = false;

        while (textArchitect.isConstructing)
        {


            yield return new WaitForEndOfFrame();
        }
        //if skipping stopped the display text from updating correctly, force it to update at the end.


        CharacterManager.instance.character.SetAnimation(CharacterManager.instance.character.animationNormal, true);

        //text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
        {
            yield return new WaitForEndOfFrame();
        }




        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = speakerNameText.text;
        if (s != speakerNameText.text && s != "")
            retVal = (s.ToLower().Contains("narrator")) ? "" : s;

        return retVal;
    }

    public void Close()
    {
        StopSpeaking();
        foreach (GameObject ob in speechPanelRequirements)
        {
            ob.SetActive(false);
        }
    }

    public void OpenAllRequirementsForDialogueSystemVisibility(bool v)
    {
        for (int i = 0; i < speechPanelRequirements.Length; i++)
        {
            speechPanelRequirements[i].SetActive(v);
        }
    }

    public void Open(string speakerName = "", string speech = "")
    {
        if (speakerName == "" && speech == "")
        {
            OpenAllRequirementsForDialogueSystemVisibility(false);
            return;
        }
        OpenAllRequirementsForDialogueSystemVisibility(true);

        speakerNameText.text = speakerName;

        speakerNamePane.SetActive(speakerName != "" && speakerName != "narrator");

        speechText.text = speech;
    }

    public bool isClosed
    {
        get { return !speechBox.activeInHierarchy; }
    }

    [System.Serializable]
    public class ELEMENTS
    {
        public GameObject speechPanel;

        public GameObject speakerNamePane;
        public TMP_Text speakerNameText;

        public TMP_Text speechText;
    }
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public TMP_Text speakerNameText { get { return elements.speakerNameText; } }
    public TMP_Text speechText { get { return elements.speechText; } }

    public GameObject speakerNamePane { get { return elements.speakerNamePane; } }

    public GameObject[] speechPanelRequirements;
    public GameObject speechBox;
}
