using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_02_HPF_Manager : MonoBehaviour
{

    public List<MG_02_HPF_Case> cases;

    public static MG_02_HPF_Manager instance;

    void Start()
    {
        instance = this;
    }

    public void CheckAll(){
        foreach(MG_02_HPF_Case c in cases){
            if(!c.Check()){
                return;
            }
        }
        MG_Starter.instance.LoadNext();
    }
}
