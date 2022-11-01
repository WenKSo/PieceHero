using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Piece : NetworkBehaviour
{
    [Networked] public int squareId { get; set; }
    [Networked] private Vector3 position { get; set; }
    public Square currentSquare;
    public ColorType color;
    [Networked]
    private NetworkBool Finished { get; set; }

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
            squareId = currentSquare.id;  
        }
        updatePos(); 
    }

    public void updatePos()
    {
        Vector3 pos = currentSquare.transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }

    // public Square findSquare(int id)
    // {
    //     return map[id];
    // }

    public override void FixedUpdateNetwork()
    {
        if(Runner.IsServer)
        {
            position = transform.position;
        }
        transform.position = position;
    } 
}
