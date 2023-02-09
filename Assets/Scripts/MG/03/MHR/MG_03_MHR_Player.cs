using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_MHR_Player : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        if(!MG_Starter.instance.canStart){
            return;
        }
        
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if(! (movement.x == 0) || !(movement.y == 0)){
            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
        }

        animator.SetFloat("Speed",movement.sqrMagnitude);
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime);
    }
}
