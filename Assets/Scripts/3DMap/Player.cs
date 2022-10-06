using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Square currentSquare;
    public ColorType color;

    public void move(int steps){
        //当已经在最后的胜利通道时
        Square temp = currentSquare;
        int rsteps = steps;
        for(int i=0;i<steps;i++){
            temp = temp.next;
            if(currentSquare.type == SquareType.Finish && i<(steps-1))
            {
                rsteps = i;
                break;
            }
        }

        for(int i=0;i<rsteps;i++)
        {
            if(currentSquare.type == SquareType.Special)
            {
                currentSquare = currentSquare.next2;
            }
            else
            {
                currentSquare = currentSquare.next;
            }
            updatePos();   
        }
    }

    public void updatePos()
    {
        Vector3 pos = currentSquare.transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    } 
}
