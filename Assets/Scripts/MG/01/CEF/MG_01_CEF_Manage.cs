using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_01_CEF_Manage : MonoBehaviour
{
    public static MG_01_CEF_Manage instance;

    public List<Transform> spawns;

    public GameObject prefab;

    float cooldown = 0;

    public float MaxCooldown = 1f;

    public AudioClip normal;
    public AudioClip wrong;

    public int Score = 0;

    public UnityEngine.UI.Text text;

    public int TargetScore = 200;

    [HideInInspector]
    public bool IgnoreFist = true;


    void Awake()
    {
        instance = this;
        AddScore(0);
    }

    void Update()
    {
        if(!MG_Starter.instance.canStart){
            return;
        }
        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }else{
            cooldown=MaxCooldown;
            GameObject ob = Instantiate(prefab,spawns[Random.Range(0,spawns.Count)].position,new Quaternion(),transform);
            ob.GetComponent<MG_01_CEF_Note>().speed = Random.Range(3,6);
        }
    }

    public void AddScore(int p){
        Score = Mathf.Clamp(Score+p,0,TargetScore);
        text.text = "Score : "+Score.ToString()+"/"+TargetScore.ToString();
        if(Score>=TargetScore){
            MG_Starter.instance.LoadNext();
        }
    }

    public void PlayNormal(){
        if(normal == null){
            return;
        }
        AudioSource source = CreateNewSource();
        source.clip = normal;
        source.volume = 1f;
        source.pitch = Random.Range(0.5f,1f);
        source.Play();
        Destroy(source.gameObject,normal.length);
    }

    public void PlayWrong(){
        if(wrong == null){
            return;
        }
        AudioSource source = CreateNewSource();
        source.clip = wrong;
        source.volume = 1f;
        source.pitch = Random.Range(0.5f,1f);
        source.Play();

        Destroy(source.gameObject,normal.length);
    }

    AudioSource CreateNewSource(){
        AudioSource newSource = new GameObject("sound").AddComponent<AudioSource>();
        newSource.transform.SetParent(instance.transform);
        return newSource;
    }
}
