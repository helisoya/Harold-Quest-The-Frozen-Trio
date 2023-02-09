using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_03_ST_Manager : MonoBehaviour
{

    public float pv;
    float maxPV;
    public float temps;
    float maxTemps;

    public int speedDrain;

    public int valGain;

    public Image tempsFill;

    public Image imgBar;


    void Start()
    {
        maxTemps = temps;
        maxPV = pv;
    }


    void Update()
    {
        if (!MG_Starter.instance.canStart)
        {
            return;
        }

        pv = Mathf.Clamp(pv - Time.deltaTime * speedDrain, 0, maxPV);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pv = Mathf.Clamp(pv + valGain, 0, maxPV);
        }

        if (pv <= 0)
        {
            pv = maxPV;
            temps = maxTemps;
        }

        if (temps < 0)
        {
            MG_Starter.instance.LoadNext();
        }
        else
        {
            temps -= Time.deltaTime;
        }

        imgBar.fillAmount = pv / maxPV;
        tempsFill.fillAmount = temps / maxTemps;


    }
}
