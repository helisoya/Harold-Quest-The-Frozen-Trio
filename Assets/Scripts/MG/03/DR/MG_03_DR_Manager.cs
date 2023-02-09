using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_DR_Manager : MonoBehaviour
{
    public string[] paroles;
    public int[] choix_intervales;
    int currentP = 0;
    int currentC = 0;

    public Text text;
    public Text[] choix;

    public GameObject holder_rap;
    public GameObject holder_choix;

    public Choix_Rap[] tabChoix; 

    void Start()
    {
        StartCoroutine(Rap());
    }

    IEnumerator Rap(){
        holder_choix.SetActive(false);
        holder_rap.SetActive(true);
        while(!MG_Starter.instance.canStart){
            yield return new WaitForEndOfFrame();
        }
        while(choix_intervales[currentC] != currentP){
            text.text = paroles[currentP];
            yield return new WaitForSeconds(2);
            currentP++;
        }
        holder_choix.SetActive(true);
        holder_rap.SetActive(false);
        for(int i = 0;i<4;i++){
            choix[i].text = tabChoix[currentC].awnsers[i];
        }

    }

    public void CheckChoix(int val){
        if(val == tabChoix[currentC].correctAwnser){
            currentC++;
            currentP++;
        }else{
            currentP = 0;
            currentC = 0;
        }
        if(currentC == choix_intervales.Length){
            MG_Starter.instance.LoadNext();
            return;
        }
        StartCoroutine(Rap());
    }
}


[System.Serializable]
public class Choix_Rap{
    public string[] awnsers = new string[4];
    public int correctAwnser;
}