using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_02_CBFF_Manager : MonoBehaviour
{
    public int restant = 10;
    public Text textRestant;

    public Text textTemps;

    public float temps = 10;

    float maxTemps;
    public List<Color> colors;
    public List<Image> order_Images;

    public List<Image> choice_Images;

    public List<int> choices;

    public List<int> currentOrder;

    void Start(){
        maxTemps = temps;
        NewOrder();
    }

    void Update(){
        if(!MG_Starter.instance.canStart){
            return;
        }
        
        if(temps < 0){
            temps = maxTemps;
            NewOrder();
        }else{
            temps-=Time.deltaTime;
        }
        textTemps.text = "Temps : "+((int)temps+1).ToString();
    }

    void NewOrder(){
        for(int i =0;i<choices.Count;i++){
            choices[i] = 0;
            currentOrder[i] = Random.Range(0,colors.Count);
            choice_Images[i].color = colors[0];
            order_Images[i].color = colors[currentOrder[i]];
        }
    }

    public void ChangeChoice(int index){
        choices[index]++;
        if(choices[index] == colors.Count){
            choices[index] = 0;
        }
        choice_Images[index].color = colors[choices[index]];
    }

    public void CheckIfOK(){
        for(int i = 0;i<choices.Count;i++){
            if(choices[i] != currentOrder[i]){
                return;
            }
        }
        temps = maxTemps;
        restant--;
        if(restant == 0){
            MG_Starter.instance.LoadNext();
        }else{
            textRestant.text = "Restant : "+restant.ToString();
            NewOrder();
        }

        
    }

}
