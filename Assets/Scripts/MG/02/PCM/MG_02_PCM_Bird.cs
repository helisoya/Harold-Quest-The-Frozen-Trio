using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_02_PCM_Bird : MonoBehaviour
{
    public Vector3 target;
    public int speed;

    public int score;

    void Update()
    {
       if(transform.position != target){
           transform.position = Vector3.MoveTowards(transform.position,target,Time.deltaTime*speed);
       }else{
           Destroy(gameObject);
       }
    }

    void OnMouseDown(){
        MG_02_PCM_Manager.instance.AddScore(score);
        Destroy(gameObject);
    }
}
