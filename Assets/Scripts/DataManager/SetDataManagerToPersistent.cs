using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDataManagerToPersistent : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
