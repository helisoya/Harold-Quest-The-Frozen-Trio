using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_PPR_Bouton : MonoBehaviour
{
    int val = 0;


    public void UpdateValues(MG_01_PPR_Phrase phrase){
        val = phrase.valeur;
        GetComponent<UnityEngine.UI.Text>().text = phrase.nom;
    }

    public void MakeChoice(){
        MG_01_PPR_Manager.instance.AddScore(val);
    }
}
