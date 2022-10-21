using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Red,
    Yellow,
    Blue,
    Green
}

public class Square : MonoBehaviour
{
    public int id;
    public Square next;
    public Square next2;
    public SquareType type;
    public ColorType color;

    public virtual void transferTo(Piece piece){}

    public void jump(Piece piece)
    {
        if(piece.color == color && type != SquareType.Special)
        {
            piece.move(4);
        }    
    }
}
 