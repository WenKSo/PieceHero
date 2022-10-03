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

    public override void transferTo(Player player)
    {  
        player.currentSquare = destination;
        player.updatePos();
    }
}
