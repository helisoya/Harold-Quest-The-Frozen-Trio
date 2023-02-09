using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_CC_Player : MonoBehaviour
{

    public Rigidbody2D rb;

    public float forceJump;

    public float speed;

    bool isGround = true;

    void Update()
    {

        if(!MG_Starter.instance.canStart){
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && isGround){
            rb.AddForce(new Vector2(0,1) * forceJump);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed,-8,8),transform.position.y,transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag=="GameController"){
            isGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.tag=="GameController"){
            isGround = false;
        }
    }
}
