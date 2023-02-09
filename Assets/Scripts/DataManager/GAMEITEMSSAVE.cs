using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GAMEITEMSSAVE
{
    public List<ITEM> items;


    [System.Serializable]
    public class ITEM{
        public string name;
        public string value;

        [HideInInspector]
        public string defaultValue;
    }

}
