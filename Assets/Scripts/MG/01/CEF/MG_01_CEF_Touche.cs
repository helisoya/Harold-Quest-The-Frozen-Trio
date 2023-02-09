using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_CEF_Touche : MonoBehaviour
{
    public KeyCode code;

    bool noteInside;

    GameObject inside;
    
    void Update()
    {
        if(noteInside){
            if(Input.GetKeyDown(code)){
                MG_01_CEF_Manage.instance.PlayNormal();
                MG_01_CEF_Manage.instance.AddScore(10);
                Destroy(inside);
                noteInside = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        noteInside = true;
        inside = other.transform.gameObject;
    }

    void OnTriggerExit2D(Collider2D other){
        noteInside = false;
    }
}
