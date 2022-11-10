using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Piece : NetworkBehaviour
{
    [Networked(OnChanged = "OnSquareIDChanged")] public int squareId { get; set; }
    public Square currentSquare;
    public ColorType color;
    [Networked]
    private NetworkBool Finished { get; set; }

    public override void Spawned()
    {
        Debug.Log(squareId);
        MapManager mapManager = FindObjectOfType<MapManager>();
        currentSquare = mapManager.map[squareId];
        updatePos();
    }

    public void move(int steps)
    {
        //when it is on the road to win
        Square temp = currentSquare;
        int rsteps = steps;
        for (int i = 0; i < steps; i++)
        {
            temp = temp.next;
            if (currentSquare.type == SquareType.Finish && i < (steps - 1))
            {
                rsteps = i;
                break;
            }
        }

        for (int i = 0; i < rsteps; i++)
        {
            if (currentSquare.type == SquareType.Special)
            {
                currentSquare = currentSquare.next2;
            }
            else
            {
                currentSquare = currentSquare.next;
            }
            squareId = currentSquare.id;
        }
    }

    public void updatePos()
    {
        Vector3 pos = currentSquare.transform.position;
        transform.position = pos;
    }

    public static void OnSquareIDChanged(Changed<Piece> changed)
    {
        changed.Behaviour.OnSquareIDChanged();
    }

    private void OnSquareIDChanged()
    {
        MapManager mapManager = FindObjectOfType<MapManager>();
        currentSquare = mapManager.map[squareId];
        updatePos();
    }

    public override void FixedUpdateNetwork()
    {
        /**
        if(Runner.IsServer)
        {
            position = transform.position;
        }
        transform.position = position;
    } **/
    }
}
