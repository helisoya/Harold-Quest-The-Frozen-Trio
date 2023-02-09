using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_MHR_Monster : MonoBehaviour
{
    public Transform player;

    public float speed;

    public Animator animator;

    Vector2 movement;


    void Update(){
        transform.position = Vector2.MoveTowards(transform.position,player.position,Time.deltaTime*speed);

        float dist = -Vector2.Distance(transform.position,player.position);

        animator.SetFloat("Horizontal",(movement.x-player.position.x)/dist);
        animator.SetFloat("Vertical",(movement.y-player.position.y)/dist);

    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            MG_03_MHR_Manager.instance.posX = 0;
            MG_03_MHR_Manager.instance.posY = 0;
            MG_03_MHR_Manager.instance.LoadRoom(0,0);
            gameObject.SetActive(false);
        }
    }
}
