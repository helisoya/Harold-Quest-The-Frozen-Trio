using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_T_Spawn : MonoBehaviour
{
    public GameObject[] tetrisPrefabs;

    public int remaining = 21;

    public UnityEngine.UI.Text text;
    void Start()
    {
        NewTetromino();
    }

    public void NewTetromino(){
        remaining--;
        text.text = "Restant : "+remaining.ToString();
        if(remaining==0){
            MG_Starter.instance.LoadNext();
            return;
        }
        Instantiate(tetrisPrefabs[Random.Range(0,tetrisPrefabs.Length)],transform.position,Quaternion.identity);
    }
}
