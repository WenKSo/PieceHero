using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Square currentSquare;

    public void move(int steps){
        for(int i=0;i<steps;i++)
        {
            currentSquare = currentSquare.next;
        }
    }
}
