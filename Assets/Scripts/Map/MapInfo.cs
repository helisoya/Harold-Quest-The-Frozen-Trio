using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    [SerializeField] private LocalizedText text;

    public void UpdateInfo(bool show, string key)
    {
        gameObject.SetActive(show);
        text.SetNewKey(key);

        if (show == true) transform.position = Input.mousePosition;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.position = Input.mousePosition;
        }
    }
}
