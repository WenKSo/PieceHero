using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Web.Script.Serialization;

public class Map : MonoBehaviour
{
    //public string Name;
    public int length;
    public int width;
    public BlockData[] blocks;

    public void SaveToString()
    {
        var json = new JavaScriptSerializer().Serialize(this);
        Console.WriteLine(json);
    }
}
