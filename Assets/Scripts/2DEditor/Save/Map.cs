using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Map : MonoBehaviour
{
    //public string Name;
    public int length;
    public int width;
    public BlockData[] blocks;

    public void SaveToString()
    {
        string json = JsonHelper.ToJson(blocks);
        Debug.Log(json);
    }
}
