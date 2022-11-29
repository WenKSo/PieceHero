using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorManager : MonoBehaviour
{
    public GameObject Map;
    private int width;
    private int length;

    public GameObject block;
    public TMP_InputField widthText;
    public TMP_InputField lengthText;
    public GameObject MapSetting;
    public GameObject BlockPanel;

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
        width =  int.Parse(widthText.text);
        length = int.Parse(lengthText.text);
        for (int x=-length / 2; x< length / 2; ++x)
        {
            for (int y=-width / 2; y< width / 2; ++y)
            {
                Instantiate(block, new Vector3(x,y,0), Quaternion.identity);
            }
        }
        BlockPanel.SetActive(true);
        MapSetting.SetActive(false);
   }

    //Save Map Data
    public void Save()
    {
        Map map = Map.GetComponent<Map>();
        map.width = width;
        map.length = length;
        Block[] bd = GameObject.FindObjectsOfType<Block>();
        map.blocks = new BlockData[width*length];
        for (int i = 0; i < bd.Length; i++)
        {
            map.blocks[i] = new BlockData(bd[i].type, bd[i].Up, bd[i].Down, bd[i].Left, bd[i].Right);
        }
        map.SaveToString();
    }

}
