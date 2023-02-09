using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_CC_Tank : MonoBehaviour
{
    public GameObject prefabTir;

    public float cooldown;

    float maxCooldown;

    public int speed;

    public Transform canon;

    void Start()
    {
        maxCooldown = cooldown;
        StartCoroutine(Tank());
    }
    IEnumerator Tank(){
        if(!MG_Starter.instance.canStart){
            yield return new WaitForEndOfFrame();
        }

        while(true){
            while(cooldown>0){
                cooldown-=Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            cooldown = maxCooldown;

            while(transform.position.x <= -7){
                transform.position+=new Vector3(1,0,0) * speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(0.5f);

            Instantiate(prefabTir,canon);

            yield return new WaitForSeconds(0.5f);

            while(transform.position.x >= -12){
                transform.position+=new Vector3(-1,0,0) * speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            MG_03_CC_Manager.instance.AddTemps(2);
        }
    }
}
