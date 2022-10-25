using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MapManager : NetworkBehaviour
{
    public Piece piece;
    public NetworkObject nick;
    public Square[] map;

    void Start()
    {
        assignId();
    }

    private void findPiece()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach(GameObject p in pieces)
        {
            if(p.GetComponent<NetworkObject>().HasInputAuthority)
            {
                piece = p.GetComponent<Piece>();
                return;
            }
        }
    }

    public void row()
    {   
        findPiece(); 
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        Debug.Log(rInt);
        piece.currentSquare = map[piece.squareId];
        piece.move(rInt);
        piece.squareId = piece.currentSquare.id;
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

    public void assignId()
    {
        map = FindObjectsOfType<Square>();
        int count = 0;
        foreach (Square i in map)
        {
            i.id = count;
            count++;
        }
    }

    // public void rowtest()
    // {
    //     var r = new System.Random();
    //     int rInt = r.Next(1, 7);
    //     nick.GetComponent<NicknameText>().SetNick(rInt);
    // }
}
