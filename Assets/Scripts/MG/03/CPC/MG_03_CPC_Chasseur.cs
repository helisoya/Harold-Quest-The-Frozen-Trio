using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_03_CPC_Chasseur : MonoBehaviour
{

    public int speed;

    public GameObject prefab;

    public int bulletSpeed;

    public float cooldown;

    float maxCooldown;

    int target = 0;

    Coroutine moving = null;

    public Transform barrel;


    void Start()
    {
        maxCooldown = cooldown;
    }

    IEnumerator Moving(float x)
    {
        Vector3 t = new Vector3(x, transform.position.y, transform.position.z);
        while (transform.position.x != x)
        {
            transform.position = Vector3.MoveTowards(transform.position, t, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        moving = null;
    }

    void Update()
    {
        if (!MG_Starter.instance.canStart)
        {
            return;
        }

        if (MG_01_A_Player.instance.position != target)
        {
            target = MG_01_A_Player.instance.position;
            if (moving != null)
            {
                StopCoroutine(moving);
            }
            moving = StartCoroutine(Moving(MG_01_A_Player.instance.positions[target].transform.position.x));
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = maxCooldown;
            GameObject ob = Instantiate(prefab, MG_01_A_Manager.instance.transform);
            ob.transform.position = barrel.position;
            ob.GetComponent<MG_01_A_Rocher>().speed = bulletSpeed;
        }
    }
}
