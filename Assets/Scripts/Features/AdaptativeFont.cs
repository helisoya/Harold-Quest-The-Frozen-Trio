using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeFont : MonoBehaviour
{

    TMPro.TMP_Text text;

    public bool continualUpdate = true;

    public int fontSizeAtDefaultREsolution = 20;
    public static float defaultResolution = 2525f;


    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();

        if(continualUpdate){
            InvokeRepeating("Adjust",0f,Random.Range(0.5f,2f));
        }else{
            Adjust();
            enabled = false;
        }
    }
        void Adjust(){
            if(!enabled || gameObject.activeInHierarchy){
                return;
            }
            float totalCurrentRes = Screen.height+Screen.width;
            float perc = totalCurrentRes/defaultResolution;

            int fontsize = Mathf.RoundToInt((float)fontSizeAtDefaultREsolution * perc);
            text.fontSize = fontsize;
        }
}
