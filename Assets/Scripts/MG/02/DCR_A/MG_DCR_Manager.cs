using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DCR_Manager : MonoBehaviour
{

    public int maxHp;

    int playerHp;

    int ennemyHp;

    public GameObject choices;

    public UnityEngine.UI.Text text;

    public UnityEngine.UI.Image hpbar_player;

    public UnityEngine.UI.Image hpbar_ennemi;

    public List<string> phrases_ennemi;

    public List<string> phrases_faible;

    public List<string> phrases_forte;

    public void AttaqueForte(){
        text.text = "Vous attaquez de toute vos forces !";
        ennemyHp-=Random.Range(8,21);
        RefreshHP();
        StartCoroutine(Bot());
    }

    public void AttaqueFaible(){
        text.text = "Vous attaquez l'ennemi !";
        ennemyHp-=Random.Range(10,13);
        RefreshHP();
        StartCoroutine(Bot());
    }

    void RefreshHP(){
        hpbar_ennemi.fillAmount = (float)ennemyHp/(float)maxHp;
        hpbar_player.fillAmount = (float)playerHp/(float)maxHp;
    }

    IEnumerator Bot(){
        choices.SetActive(false);
        yield return new WaitForSeconds(1);
        if(ennemyHp<=0){
            MG_Starter.instance.LoadNext();
        }
        if(Random.Range(0,2) == 0){
            playerHp-=Random.Range(10,13);
            text.text = phrases_faible[Random.Range(0,Random.Range(0,phrases_faible.Count))];
        }else{
            playerHp-=Random.Range(8,21);
            text.text = phrases_forte[Random.Range(0,Random.Range(0,phrases_forte.Count))];
        }

        if(playerHp <= 0){
            playerHp = maxHp;
            ennemyHp = maxHp;
        }
        RefreshHP();
        yield return new WaitForSeconds(1);

        text.text = phrases_ennemi[Random.Range(0,Random.Range(0,phrases_ennemi.Count))];
        choices.SetActive(true);
    }

    void Start()
    {
        playerHp = maxHp;
        ennemyHp = maxHp;
    }
}
