using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class MapManager : MonoBehaviour
{
    public Piece piece;
    public NetworkObject nick;
    public Square[] map;
    public TMP_Text RollNumText;

    void Start()
    {
        assignId();
    }

    public void roll()
    {   
        var r = new System.Random();
        int rInt = r.Next(1, 7);
        Log.Debug(rInt);
        RollNumText.text = rInt.ToString();
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
