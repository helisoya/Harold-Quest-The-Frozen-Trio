using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_LF_Manager : MonoBehaviour
{
    public GameObject text;

    Coroutine wrong = null;
    public void Try(){
        if(Random.Range(1,4)==1){
            MG_Starter.instance.LoadNext();
        }else{
            if(wrong != null){
                StopCoroutine(wrong);
            }
            wrong = StartCoroutine(SayWrong());
        }
    }

    IEnumerator SayWrong(){
        text.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(false);
        wrong = null;   
    }
}
