using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo : MonoBehaviour
{
    [SerializeField] private LocalizedText questName;
    [SerializeField] private LocalizedText questObjective;

    public void Init(string id, string value)
    {
        questName.SetNewKey(id);
        questObjective.SetNewKey(id + "_" + value);
    }
}
