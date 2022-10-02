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
            Vector3 pos = currentSquare.transform.position;
            pos.y = transform.position.y;
            transform.position = pos; 
        }
        if(currentSquare.type == SquareType.Finish){
            Debug.Log("You win!");
        }
    }
}
