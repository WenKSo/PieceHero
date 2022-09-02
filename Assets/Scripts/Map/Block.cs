using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block: MonoBehaviour
{
    //Basic attributes
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
    public BlockType type;

    // Sprites
    public SpriteRenderer spriteRenderer;
    public Sprite blank;
    public Sprite select;
    public Sprite plain;
    public Sprite start;
    public Sprite finish;
    public Sprite shop;
    public Sprite luck;
    public Sprite chance;
    public Sprite teleport;
    
    public void ChangeSprite(int selectType)
    {
        switch(selectType)
        {
            case 0:
                type = BlockType.Blank;
                spriteRenderer.sprite = blank; 
                break;
            case 1:
                spriteRenderer.sprite = select; 
                break;
            case 2:
                type = BlockType.Plain;
                spriteRenderer.sprite = plain; 
                break;
            case 3:
                type = BlockType.Start;
                spriteRenderer.sprite = start; 
                break;
            case 4:
                type = BlockType.Finish;
                spriteRenderer.sprite = finish; 
                break;
            case 5:
                type = BlockType.Shop;
                spriteRenderer.sprite = shop; 
                break;
            case 6:
                type = BlockType.Luck;
                spriteRenderer.sprite = luck; 
                break;
            case 7:
                type = BlockType.Chance;
                spriteRenderer.sprite = chance; 
                break;
            case 8:
                type = BlockType.Teleport;
                spriteRenderer.sprite = teleport; 
                break;
        }
        
    }

    //Constructor
    public Block(BlockType bt){
        type = bt;
    }
}
