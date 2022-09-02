using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorManager : MonoBehaviour
{
    public GameObject block;
    public TMP_InputField width;
    public TMP_InputField length;

    //All blocks
    public GameObject plain;
    public GameObject shop;
    public GameObject chance;
    public GameObject finish;
    public GameObject teleport;
    public GameObject luck;
    public GameObject start;

    // Select block Lock
    private bool select;
    public GameObject selectedObject;
    public int currentType; // 0 for blank, 1 for select, 2 for normal ...

    public void CreateBlock(int type)
   {
        if(!select)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            switch(type) 
            {
                case 2:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(plain, mousePosition, Quaternion.identity);
                    break;
                case 3:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(start, mousePosition, Quaternion.identity);
                    break;
                case 4:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(finish, mousePosition, Quaternion.identity);
                    break;
                case 5:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(shop, mousePosition, Quaternion.identity);
                    break;
                case 6:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(luck, mousePosition, Quaternion.identity);
                    break;
                case 7:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(chance, mousePosition, Quaternion.identity);
                    break;
                case 8:
                    select = true;
                    currentType = type;
                    selectedObject = Instantiate(teleport, mousePosition, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
        
   } 

   //Create Canvas for putting blocks into it 
   public void CreateCanvas()
   {
        int w =  int.Parse(width.text);
        int l = int.Parse(length.text);
        for (int x=0; x<l; ++x)
        {
            for (int y=0; y<w; ++y)
            {
                Instantiate(block, new Vector3(x-3,y-1,0), Quaternion.identity);
            }
        }
   }
   
   Block lastTarget;
   void Update()
   {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        if(select)
        {
            selectedObject.transform.position = mousePosition;
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            
            if(targetObject && targetObject.GetComponent<Block>().type == BlockType.Blank)
            {
                Block target = targetObject.GetComponent<Block>();
                target.ChangeSprite(1);
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log(currentType);
                    target.ChangeSprite(currentType);
                    select = false;
                    Destroy(selectedObject);
                }
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            select = false;
            Destroy(selectedObject); 
        }
   }
}
