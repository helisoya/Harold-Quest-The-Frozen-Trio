using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_TSS_Manager : MonoBehaviour
{
    public int restant = 10;
    
    public Text text;

    public GameObject[] ennemy;

    Coroutine timer = null;

    int current = 0;

    void Start(){
        PlaceEnnemyRandomly();
        timer = StartCoroutine(TimerToClick());
    }

    void PlaceEnnemyRandomly(){
        ennemy[current].SetActive(false);
        int de = Random.Range(0,ennemy.Length);
        while(de == current){
            de = Random.Range(0,ennemy.Length);
        }
        current = de;
        ennemy[current].SetActive(true);
    }

    public void ClickOnEnnemy(){
        StopCoroutine(timer);
        restant--;
        if(restant==0){
            MG_Starter.instance.LoadNext();
            return;
        }
        text.text = "Restant : "+restant.ToString();
        PlaceEnnemyRandomly();
        timer = StartCoroutine(TimerToClick());
    }

    IEnumerator TimerToClick(){
        while(!MG_Starter.instance.canStart){
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        PlaceEnnemyRandomly();
        timer = StartCoroutine(TimerToClick());
    }
}
