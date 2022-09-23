using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public void row(){
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        Debug.Log(rInt);
        //return rInt;
    }
}
