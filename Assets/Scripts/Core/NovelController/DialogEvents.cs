using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvents : MonoBehaviour
{
    public static void HandleEvent(string _event, CLM.LINE.SEGMENT segment){

        if(_event.Contains("(")){
            string[] actions = _event.Split(' ');
            for(int i =0;i<actions.Length;i++){
                NovelController.instance.HandleAction(actions[i]);
            }
        }

        string[] eventData = _event.Split(' ');

        switch(eventData[0]){
            case "txtSpd":
                EVENT_TxtSpd(eventData[1],segment);
                break;
            case "/txtSpd":
                segment.architect.speed = 5;
                segment.architect.charactersPerFrame = 1;
                break;
        }
    }

    static void EVENT_TxtSpd(string data, CLM.LINE.SEGMENT seg){
        string[] parts = data.Split(',');
        float delay = float.Parse(parts[0],System.Globalization.CultureInfo.InvariantCulture);
        int characterPerFrame = int.Parse(parts[1],System.Globalization.CultureInfo.InvariantCulture);
        seg.architect.speed = delay;
        seg.architect.charactersPerFrame = characterPerFrame;
    }
}
