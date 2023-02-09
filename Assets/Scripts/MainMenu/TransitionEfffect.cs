using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionEfffect : MonoBehaviour
{
    Image image;

    public Material transitionMaterialPrefab;

    Coroutine transition = null;


    public Texture2D transitionEffect;

    public Image TransitionLayer;

    void Awake()
    {
        image = GetComponent<Image>();
        TransitionLayer.sprite = image.sprite;
    }

    public void TransitionToTex(Sprite newTex){
        if(transition != null){
            StopCoroutine(transition);
        }
        transition = StartCoroutine(Transitioning(newTex));
    }

    IEnumerator Transitioning(Sprite newTex){
        TransitionLayer.sprite = image.sprite;
        image.sprite = newTex;
        TransitionLayer.material = new Material(transitionMaterialPrefab);
        TransitionLayer.material.SetTexture("_AlphaTex",transitionEffect);
        TransitionLayer.material.SetFloat("_Cutoff",1);
        float curVal = 0;

        while(curVal < 1){
            curVal = Mathf.MoveTowards(curVal,1,1 * Time.deltaTime);
            TransitionLayer.material.SetFloat("_Cutoff",curVal);
            yield return new WaitForEndOfFrame();
        }
        transition = null;
    }
}
