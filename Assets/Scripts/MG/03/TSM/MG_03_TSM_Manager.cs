using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_TSM_Manager : MonoBehaviour
{
    public int targetScore = 500;
    int score = 0;
    public Text text;
    public Transform marchand;

    void Fire(){
        float marchandPos = Mathf.Abs(marchand.position.x);
        print(marchandPos);
        if(marchandPos <= 2){
            score = 0;
        }else if(marchandPos <= 3){
            score += 100;
        }else if(marchandPos <= 4){
            score += 50;
        }else if(marchandPos <= 6){
            score += 20;
        }

        text.text = "Score : "+score.ToString()+"/"+targetScore;ToString();

        if(score >=targetScore){
            MG_Starter.instance.LoadNext();
        }
    }

    void Update()
    {
        if(!MG_Starter.instance.canStart){
            return;
        }

        if(Input.GetMouseButtonDown(0)){
            Fire();
        }
    }
}
