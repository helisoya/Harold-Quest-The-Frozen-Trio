using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_CC_Manager : MonoBehaviour
{
    public Image barFill;

    public float temps;

    float maxTemps;

    public static MG_03_CC_Manager instance;

    public GameObject obstaclePrefab;

    public float cooldown;

    float maxCooldown;

    void Start(){
        maxTemps = temps;
        maxCooldown = cooldown;
        instance = this;
    }

    public void AddTemps(float value){
        temps = Mathf.Clamp(temps+value,0,maxTemps);
    }

    void Update()
    {
        if(!MG_Starter.instance.canStart){
            return;
        }

        AddTemps(-Time.deltaTime);
        if(temps == 0){
            MG_Starter.instance.LoadNext();
            return;
        }

        barFill.fillAmount = temps/maxTemps;

        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }else{
            cooldown = maxCooldown;
            GameObject ob = Instantiate(obstaclePrefab,transform);
            ob.transform.position = new Vector3(9,-2.39f,0);
        }

    }
}
