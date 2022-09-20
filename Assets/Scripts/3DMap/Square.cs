using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Square next;
    public SquareType type;

    //Constructor
    public Square(SquareType st)
    {
        type = st;
    }
    
     
}
