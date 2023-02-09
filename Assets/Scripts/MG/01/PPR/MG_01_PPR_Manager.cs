using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_01_PPR_Manager : MonoBehaviour
{

    public int current = 0;

    public int score = 0;

    public static MG_01_PPR_Manager instance;

    void Awake(){
        instance = this;
        RearangeWords();
    }

    public List<MG_01_PPR_Bouton> boutons;

    public List<MG_01_PPR_Phrase> phrases;

    public Text currText;

    public Text scoreText;


    public void AddScore(int val){
        score+= val;
        current++;
        scoreText.text = "Score : "+score.ToString();
        currText.text = current.ToString()+"/20";
        if(current<=20){
            RearangeWords();
        }else{
            if(score < 20){
                RearangeWords();
                current = 0;
                score = 0;
                scoreText.text = "Score : "+score.ToString();
                currText.text = current.ToString()+"/20";
            }else{
                MG_Starter.instance.LoadNext();
            }
        }
    }

    public void RearangeWords(){
        List<MG_01_PPR_Phrase> copy = new List<MG_01_PPR_Phrase>(phrases);

        foreach(MG_01_PPR_Bouton text in boutons){
            int i = Random.Range(0,copy.Count);
            text.UpdateValues(copy[i]);
            copy.RemoveAt(i);
        }
    }

}


[System.Serializable]
public class MG_01_PPR_Phrase{
    public string nom;
    public int valeur;
}