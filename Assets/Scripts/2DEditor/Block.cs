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

    //Prefabs
    public GameObject arrowLeft;
    public GameObject arrowRight;
    public GameObject arrowUp;
    public GameObject arrowDown;

    //References for arrows
    private GameObject refLeft;
    private GameObject refRight;
    private GameObject refUp;
    private GameObject refDown;

    //Constructor
    public Block(BlockType bt)
    {
        type = bt;
    }

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

    private void OnMouseOver()
    {
        if(type == BlockType.Blank)
        {
            spriteRenderer.sprite = select;
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            Clear();
        }

        if (Input.GetMouseButton(0) && type != BlockType.Blank)
        {
            if(Left == false && Input.GetAxis("Mouse X") < -0.2)
            {
                Left = true;
                refLeft = Instantiate(arrowLeft, transform.position, Quaternion.identity);
            }

            if (Right == false && Input.GetAxis("Mouse X") > 0.2)
            {
                Right = true;
                refRight = Instantiate(arrowRight, transform.position, Quaternion.identity);
            }

            if (Up == false && Input.GetAxis("Mouse Y") > 0.2)
            {
                Up = true;
                refUp = Instantiate(arrowUp, transform.position, Quaternion.identity);
            }

            if (Down == false && Input.GetAxis("Mouse Y") < -0.2)
            {
                Down = true;
                refDown = Instantiate(arrowDown, transform.position, Quaternion.identity);
            }

        }
    }
    
    private void OnMouseExit()
    {
        if (type == BlockType.Blank)
        {
            spriteRenderer.sprite = blank;
        }
    }

    public void Clear()
    {
        spriteRenderer.sprite = blank;
        type = BlockType.Blank;
        Up = false;
        Down = false;
        Left = false;
        Right = false;
        Destroy(refLeft);
        Destroy(refRight);
        Destroy(refUp);
        Destroy(refDown);
    }
}
