using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_A_Rocher : MonoBehaviour
{
    public int speed;

    new SpriteRenderer renderer;

    void Start(){
        renderer = GetComponent<SpriteRenderer>();
    }   

    void Update()
    {
        transform.position+= (-transform.up) * Time.deltaTime * speed;
        if(!renderer.isVisible){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        MG_01_A_Player.instance.TakeDamage();
        Destroy(gameObject);
    }
}
