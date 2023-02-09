using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_MHR_Page : MonoBehaviour
{
    public int page;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
