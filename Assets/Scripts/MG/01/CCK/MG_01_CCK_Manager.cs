using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_01_CCK_Manager : MonoBehaviour
{
    public static MG_01_CCK_Manager instance;

    public GameObject prefab;

    float cooldown = 0;

    public float MaxCooldown = 1f;

    public Image tempsImg;

    public Text textHP;

    public float temps = 30f;

    float maxTemps;

    public int hp = 5;

    int maxHP;


    void Awake()
    {
        instance = this;
        maxTemps = temps;
        maxHP = hp;
    }

    public void RemoveHp()
    {
        hp--;
        if (hp == 0)
        {
            hp = maxHP;
            temps = maxTemps;
            tempsImg.fillAmount = temps / maxTemps;
        }
        textHP.text = hp.ToString();
    }

    void Update()
    {
        if (!MG_Starter.instance.canStart)
        {
            return;
        }
        temps -= Time.deltaTime;
        tempsImg.fillAmount = temps / maxTemps;
        if (temps <= 0)
        {
            MG_Starter.instance.LoadNext();
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = MaxCooldown;
            GameObject ob = Instantiate(prefab, new Vector3(Random.Range(-11f, 11f), 8, 0), new Quaternion(), transform);
            ob.GetComponent<MG_01_CCK_Shuriken>().speed = Random.Range(4, 6);
            ob.GetComponent<MG_01_CCK_Shuriken>().target = new Vector3(Random.Range(-9f, 9f), -8, 0);
            ob.transform.Rotate(new Vector3(0, 0, Random.Range(0, 180)));
        }
    }
}
