using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_CCK_Shuriken : MonoBehaviour
{
    public Vector3 target;
    public int speed;

    void Update()
    {
       if(transform.position != target){
           transform.position = Vector3.MoveTowards(transform.position,target,Time.deltaTime*speed);
       }else{
           MG_01_CCK_Manager.instance.RemoveHp();
           Destroy(gameObject);
       }
    }

    void OnMouseDown(){
        Destroy(gameObject);
    }
}
