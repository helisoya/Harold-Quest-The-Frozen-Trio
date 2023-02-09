using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_MHR_Door : MonoBehaviour
{
    public int x;

    public int y;
    public bool canTeleport = true;

    public Transform teleportPos;

    public MG_03_MHR_Door otherSide;

    void OnTriggerEnter2D(Collider2D col){
        if(canTeleport && col.tag=="Player"){
            otherSide.canTeleport = false;
            col.transform.position = otherSide.teleportPos.position;
            MG_03_MHR_Manager.instance.LoadRoom(x,y);

            if(Random.Range(0,100) <= 30){
                MG_03_MHR_Manager.instance.monster.transform.position = teleportPos.position;
                MG_03_MHR_Manager.instance.monster.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.tag=="Player"){
            canTeleport = true;
        }
    }
}
