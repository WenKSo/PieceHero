using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MapManager : MonoBehaviour
{
    public Piece piece;
    public NetworkObject nick;

    public void row()
    {
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        Debug.Log(rInt);
        piece.move(rInt);
        switch(piece.currentSquare.type) 
        {
            case SquareType.Finish:
                Debug.Log("You win!");
                break;
            case SquareType.Teleport:
                piece.currentSquare.transferTo(piece);
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

    public void rowtest()
    {
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        nick.GetComponent<NicknameText>().SetNick(rInt);
    }
}
