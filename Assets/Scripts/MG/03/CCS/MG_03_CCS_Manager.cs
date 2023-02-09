using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_CCS_Manager : MonoBehaviour
{
    public float maxPV = 100;
    float pvP;
    float pvE;

    public float dmg;

    public KeyCode[] keyCodes;
    int current;

    Coroutine timer;

    public Text textCode;

    public Image imgP;

    public Image imgE;

    void Start()
    {
        pvP = maxPV;
        pvE = maxPV;
        GetNewKeycode();
    }

    IEnumerator Timer(){
        while(!MG_Starter.instance.canStart){
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        pvP-=dmg;
        if(pvP <=0){
            pvP = maxPV;
            pvE = maxPV;
        }
        imgP.fillAmount = pvP/maxPV;
        GetNewKeycode();
    }

    void GetNewKeycode(){
        current = Random.Range(0,keyCodes.Length);
        textCode.text = keyCodes[current].ToString();
        timer = StartCoroutine(Timer());
    }

    void Update()
    {
        if(!MG_Starter.instance.canStart){
            return;
        }

        if(Input.GetKeyDown(keyCodes[current])){
            StopCoroutine(timer);
            pvE-=dmg;
            imgE.fillAmount = pvE/maxPV;

            if(pvE<= 0){
                MG_Starter.instance.LoadNext();
                return;
            }
            GetNewKeycode();
        }
    }
}
