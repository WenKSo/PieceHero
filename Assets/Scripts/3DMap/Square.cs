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
    public Square next;
    public SquareType type;
    public ColorType color;

    public virtual void transferTo(Player player){}

    public void jump(Player player)
    {
        if(player.color == color)
        {
            player.move(4);
        }    
    }
}
