using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_02_PCM_Manager : MonoBehaviour
{
    public int score = 0;
    public int scoreTarget = 500;

    public UnityEngine.UI.Text text;
    public List<GameObject> prefabs;

    public float cooldown = 1;
    float maxCooldown;
    public static MG_02_PCM_Manager instance;

    void Start()
    {
        maxCooldown = cooldown;
        instance = this;
    }

    void Update(){
        if(!MG_Starter.instance.canStart){
            return;
        }

        if(cooldown > 0){
            cooldown-=Time.deltaTime;
        }else{
            cooldown = maxCooldown;
            GameObject ob = Instantiate(prefabs[Random.Range(0,prefabs.Count)],transform);
            ob.transform.position = new Vector3(-10,Random.Range(-3.0f,3.0f),0);
            ob.GetComponent<MG_02_PCM_Bird>().target = new Vector3(10,Random.Range(-3.0f,3.0f),0);
            
        }
    }

    public void AddScore(int val){
        score+=val;
        if(score >= scoreTarget){
            MG_Starter.instance.LoadNext();
        }
        text.text = "Points : "+score.ToString();
    }


}
