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
        int i = 0;
        foreach(GameObject p in pieces)
        {
            if(Runner.LocalPlayer == p.GetComponent<NetworkObject>().InputAuthority)
            {
                Log.Debug("Find");
                Log.Debug(i);
                Log.Debug(Runner.LocalPlayer);
                Log.Debug(p.GetComponent<NetworkObject>().InputAuthority);
                piece = p.GetComponent<Piece>();
                return;
            }
            i++;
        }
    }

    public void roll()
    {   
        findPiece(); 
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        Debug.Log(rInt);
        piece.currentSquare = map[piece.squareId];
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

    public void onRoll()
    {
        HUDManager.instance.onClicked = !HUDManager.instance.onClicked;
    }
}
