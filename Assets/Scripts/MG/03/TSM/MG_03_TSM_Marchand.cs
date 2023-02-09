using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_TSM_Marchand : MonoBehaviour
{
    public float min;
    public float max;
    public float speed;
    int way = 1;

    Vector3 move = new Vector3(1,0,0);

    void Update()
    {
        transform.position += move * speed * Time.deltaTime * way;

        if( ( way == 1 && transform.position.x >= max ) || ( way == -1 && transform.position.x <= min )){
            way = -way;
        }
    }
}
