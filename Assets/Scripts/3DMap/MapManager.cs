using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public iont row(){
        Random r = new Random();
        int rInt = r.Next(0, 100);
        return rInt;
    }
}
