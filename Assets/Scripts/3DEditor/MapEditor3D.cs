using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor3D : MonoBehaviour
{
    public GameObject plain;

    // Select block Lock
    private bool select;
    public GameObject selectedObject;

    public void onClickBlock(int type)
    {
        if(!select)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            switch(type) 
            {
                case 0:
                    selectedObject = Instantiate(plain, mousePosition, Quaternion.identity);
                    break;
                // case 3:
                //     select = true;
                //     currentType = type;
                //     selectedObject = Instantiate(start, mousePosition, Quaternion.identity);
                //     break;
                // case 4:
                //     select = true;
                //     currentType = type;
                //     selectedObject = Instantiate(finish, mousePosition, Quaternion.identity);
                //     break;
                // case 5:
                //     select = true;
                //     currentType = type;
                //     selectedObject = Instantiate(shop, mousePosition, Quaternion.identity);
                //     break;
                // case 6:
                //     select = true;
                //     currentType = type;
                //     selectedObject = Instantiate(luck, mousePosition, Quaternion.identity);
                //     break;
                // case 7:
                //     select = true;
                //     currentType = type;
                //     selectedObject = Instantiate(chance, mousePosition, Quaternion.identity);
                //     break;
                // case 8:
                //     select = true;
                //     currentType = type;
                //     selectedObject = Instantiate(teleport, mousePosition, Quaternion.identity);
                //     break;
                default:
                    break;
            }
        }
    }

}
