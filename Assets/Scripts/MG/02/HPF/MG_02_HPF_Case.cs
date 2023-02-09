using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_02_HPF_Case : MonoBehaviour
{
    public List<int> awnser;

    int current = 0;

    public bool Check(){
        return awnser.Contains(current);
    }

    public void Rotate(){
        current++;
        if(current == 4){
            current = 0;
        }
        transform.eulerAngles = new Vector3(0,0,90*current);
        MG_02_HPF_Manager.instance.CheckAll();
    }
}
