using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MG_03_QW_Manager : MonoBehaviour
{
    public Question[] questions;


    List<Question> Arrayquestions = new List<Question>();

    public Text question;
    public Text[] buttons;

    int current;
    void Start(){
        for(int i = 0;i<questions.Length;i++){
            Arrayquestions.Add(questions[i]);
        }
        PickNewQuestion();
        RefreshTexts();
    }

    public void CheckQuestion(int val){
        if(val == Arrayquestions[current].correctAwnser){
            Arrayquestions.Remove(Arrayquestions[current]);
        }
        if(Arrayquestions.Count == 0){
            MG_Starter.instance.LoadNext();
            return;
        }
        PickNewQuestion();
        RefreshTexts();

    }

    void RefreshTexts(){
        question.text = Arrayquestions[current].question;
        for(int i =0;i<4;i++){
            buttons[i].text = Arrayquestions[current].awnsers[i];
        }
    }

    void PickNewQuestion(){
        current = Random.Range(0,Arrayquestions.Count);
    }
}



[System.Serializable]
public class Question{
    public string question;
    public string[] awnsers = new string[4];
    public int correctAwnser;
}