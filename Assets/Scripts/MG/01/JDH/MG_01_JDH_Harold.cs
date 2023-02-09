using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_01_JDH_Harold : MonoBehaviour
{
    public static MG_01_JDH_Harold instance;
    public int difficulty = 1;

    public int baseAdd = 5;

    public bool ByPassMGIfWin = false;

    public int score = 0;

    public float MaxCooldown = 1;

    float cooldown;

    public Text text;

    public SpriteRenderer haroldRenderer;

    public int AnimationFrame = 30;

    public Sprite[] textures = new Sprite[288];

    int currFrame = 0;

    

    IEnumerator AnimateHarold(){
        int cldw = 0;
        while(true){
            if(cldw>0){
                cldw--;
            }else{
                cldw = AnimationFrame;
                haroldRenderer.sprite = textures[currFrame];
                currFrame++;
                if(currFrame==288){
                    currFrame = 0;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void Start(){
        instance = this;

        cooldown = MaxCooldown;
        text.text = "Harold : "+score.ToString()+"/"+MG_01_CEF_Manage.instance.TargetScore.ToString();

        for (int i = 0;i<288;i++){
            string str = i.ToString();
            while (str.Length != 3){
                str = "0"+str;
            }
            textures[i] = Resources.Load<Sprite>("MG/01/JDH/frame_"+str+"_delay-0.06s");
        }
        StartCoroutine(AnimateHarold());
    }
    void Update()
    {
        if(!MG_Starter.instance.canStart){
            return;
        }
        if(cooldown>0){
            cooldown-=Time.deltaTime;
        }else{
            cooldown = MaxCooldown;
            AddScore(difficulty*baseAdd);

            if(score >= MG_01_CEF_Manage.instance.TargetScore){
                if(ByPassMGIfWin){
                    MG_Starter.instance.LoadNext();
                }else{
                    AddScore(-score);
                    MG_01_CEF_Manage.instance.Score = 0;
                    MG_01_CEF_Manage.instance.AddScore(0);
                }
            }
        }
    }


    public void AddScore(int val){
        score = Mathf.Clamp(score+val,0,MG_01_CEF_Manage.instance.TargetScore);
        text.text = "Harold : "+score.ToString()+"/"+MG_01_CEF_Manage.instance.TargetScore.ToString();
    }
}
