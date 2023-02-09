using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_CC_Obstacle : MonoBehaviour
{
    public float value;

    public float dirX;

    public float dirY;
    
    public float speed;


    void Update()
    {
        transform.position+=new Vector3(dirX,dirY,0) * speed * Time.deltaTime;
        if(transform.position.x <= -10 || transform.position.y <= -3 || transform.position.x >= 10 || transform.position.y >= 10){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            MG_03_CC_Manager.instance.AddTemps(value);
            Destroy(gameObject);
        }
    }
}
