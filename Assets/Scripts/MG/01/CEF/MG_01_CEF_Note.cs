using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_CEF_Note : MonoBehaviour
{
    public int speed;

    new SpriteRenderer renderer;

    void Start(){
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(Random.Range(0.6f,1),Random.Range(0.6f,1),Random.Range(0.6f,1),1);
    }   

    void Update()
    {
        transform.position+= (-transform.up) * Time.deltaTime * speed;
        if(!renderer.isVisible){
            if(MG_01_CEF_Manage.instance.IgnoreFist){
                MG_01_CEF_Manage.instance.IgnoreFist = false;
            }else{
                MG_01_CEF_Manage.instance.PlayWrong();
                MG_01_CEF_Manage.instance.AddScore(-20);
            }
            Destroy(gameObject);
        }
    }
}
