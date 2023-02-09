using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_A_Player : MonoBehaviour
{
    public static MG_01_A_Player instance;

    public int hp = 5;

    int maxHp;

    public List<GameObject> positions;

    public UnityEngine.UI.Text hpText;

    public int position;


    void Start()
    { 
        instance = this;
        maxHp = hp;
        position = positions.Count/2;
        RefreshPosition();
        hpText.text = "HP : "+hp.ToString();
    }

    void Update(){
        if(!MG_Starter.instance.canStart){
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && position != 0){
            position--;
            RefreshPosition();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) && position != positions.Count-1){
            position++;
            RefreshPosition();
        }
    }

    void RefreshPosition(){
        for(int i = 0;i<positions.Count;i++){
            if(i!=position){
                positions[i].SetActive(false);
            }else{
                positions[i].SetActive(true);
            }
        }
    }

    public void TakeDamage(){
        hp--;
        if(hp<=0){
            MG_01_A_Manager.instance.ResetTemps();
            hp = maxHp;
        }
        hpText.text = "HP : "+hp.ToString();
    }

    
}
