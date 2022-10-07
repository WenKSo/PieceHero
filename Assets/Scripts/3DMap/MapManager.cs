using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Player p1;

    public void row()
    {
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        Debug.Log(rInt);
        p1.move(rInt);
        switch(p1.currentSquare.type) 
        {
            case SquareType.Finish:
                Debug.Log("You win!");
                break;
            case SquareType.Teleport:
                p1.currentSquare.transferTo(p1);
                break;
            case SquareType.Chance:
                break;
            case SquareType.Shop:
                break;
            default:
                // code block
                break;
        }
    }
}
