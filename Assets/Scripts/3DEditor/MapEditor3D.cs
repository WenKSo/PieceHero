using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    Move,
    Connect
}

public class MapEditor3D : MonoBehaviour
{
    #region Singleton
     public static MapEditor3D Instance
     {
         get
         {
             if (instance == null)
                 instance = FindObjectOfType(typeof(MapEditor3D)) as MapEditor3D;
 
             return instance;
         }
         set
         {
             instance = value;
         }
     }
     private static MapEditor3D instance;
     #endregion

    //Block Prefabs
    public GameObject plain;
    public GameObject start;
    public GameObject end;
    public GameObject teleport;

    private Connector connector;

    // Select block Lock
    private bool select;
    public Mode currentMode = Mode.Move;
    public GameObject selectedObject;

    void Start()
    {
        connector = FindObjectOfType<Connector>();
    }

    public void onClickBlock(int type)
    {
        if(!select)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            switch(type) 
            {
                case 0:
                    selectedObject = Instantiate(plain, mousePosition+(new Vector3(0,1,0)), Quaternion.identity);
                    break;
                case 1:
                    selectedObject = Instantiate(start, mousePosition+(new Vector3(0,1,0)), Quaternion.identity);
                    break;
                case 2:
                    selectedObject = Instantiate(end, mousePosition+(new Vector3(0,1,0)), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    public void setCurrentModeMove()
    {
        currentMode = Mode.Move;
        connector.lineRend.positionCount = 0;

    }

    public void setCurrentModeConnect()
    {
        currentMode = Mode.Connect;
        connector.lineRend.positionCount = 2;
    }

}
