using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    public LocalizedText localText;

    [HideInInspector]
    public int choiceIndex = -1;
}
