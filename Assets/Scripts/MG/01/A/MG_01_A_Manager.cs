using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_01_A_Manager : MonoBehaviour
{
    public static MG_01_A_Manager instance;

    public List<Transform> spawns;

    public GameObject prefab;

    float cooldown = 0;

    public float MaxCooldown = 1f;


    public Image tempsFill;

    public float temps = 30f;

    float maxTemps;

    public int speed;

    void Start()
    {
        instance = this;
        maxTemps = temps;
    }

    public void ResetTemps()
    {
        temps = maxTemps;
        tempsFill.fillAmount = temps / maxTemps;
    }

    void Update()
    {
        if (!MG_Starter.instance.canStart)
        {
            return;
        }
        temps -= Time.deltaTime;
        tempsFill.fillAmount = temps / maxTemps;
        if (temps <= 0)
        {
            MG_Starter.instance.LoadNext();
        }

        if (prefab == null)
        {
            return;
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = MaxCooldown;
            GameObject ob = Instantiate(prefab, spawns[Random.Range(0, spawns.Count)].position, new Quaternion(), transform);
            ob.GetComponent<MG_01_A_Rocher>().speed = speed;
        }
    }

}
