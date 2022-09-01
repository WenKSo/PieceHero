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
    public GameObject normal;
    public GameObject shop;
    public GameObject chance;
    public GameObject finish;
    public GameObject teleport;
    public GameObject luck;
    public GameObject start;

    //Click and Drag
    public GameObject selectedObject;
    Vector3 offset;

    public void CreateBlock(int type)
   {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch(type) 
        {
            case 0:
                Instantiate(normal, mousePosition, Quaternion.identity);
                break;
            case 1:
                Instantiate(shop, mousePosition, Quaternion.identity);
                break;
            default:
                Instantiate(normal, mousePosition, Quaternion.identity);
                break;
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
   
   void Update()
   {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if(targetObject)
            {
                //if click on something
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject) //if not clicking
        {
            selectedObject = null;
        }
   }
}
