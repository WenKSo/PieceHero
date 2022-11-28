using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Square
{
    public Square destination;

    //Constructor
    public Teleport()
    {
        type = SquareType.Teleport;
    }

    public override void transferTo(Piece piece)
    {  
        piece.currentSquare = destination;
        piece.updatePos();
    }
}
