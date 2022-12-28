using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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

    private SaveMap save;

    private int blockCount;

    private void Awake()
    {
        Block3D_Data bd = new Block3D_Data{
            position = new Vector3(0.0f, 1.0f, 0.0f),
            id = 0,
            nextId = 0
        };
        Block3D_Data[] bds = new Block3D_Data[1];
        bds[0] = bd;
        SaveMap saveMap = new SaveMap{
            blockData = bds,
        };
        string json = JsonHelper.ToJson(bds);
        Block3D_Data[] xx = JsonHelper.FromJson<Block3D_Data>(json);
        //---------------------------
        Load();
    }

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
                    selectedObject.GetComponent<Block3D>().id = blockCount++;
                    break;
                case 1:
                    selectedObject = Instantiate(start, mousePosition+(new Vector3(0,1,0)), Quaternion.identity);
                    selectedObject.GetComponent<Block3D>().id = blockCount++;
                    break;
                case 2:
                    selectedObject = Instantiate(end, mousePosition+(new Vector3(0,1,0)), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    //------ For Mode Selection------
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
    //------ For Mode Selection------

    public void Save()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        Block3D_Data[] bs = new Block3D_Data[blocks.Length];
        save = new SaveMap{blockData = bs};
        for(int i=0;i<blocks.Length;i++)
        {
            save.blockData[i] = new Block3D_Data{
                position = blocks[i].transform.position,
                id = blocks[i].GetComponent<Block3D>().id,
                nextId = blocks[i].GetComponent<Block3D>().next.id,
            };
        }
        string json = JsonHelper.ToJson(save.blockData);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void Load()
    {
        //Load
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Block3D_Data[] blockData = JsonHelper.FromJson<Block3D_Data>(saveString);
            save = new SaveMap{blockData = blockData};
            for(int i=0;i<blockData.Length;i++)
            {
                Debug.Log(blockData[i].position);
                GameObject createdObject = Instantiate(plain, blockData[i].position, Quaternion.identity);
                createdObject.GetComponent<Block3D>().id = blockData[i].id;
            }

            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");  
            for(int i=0;i<blocks.Length;i++)
            {
                for(int j=0;j<blocks.Length;j++)
                {
                    if(blocks[j].GetComponent<Block3D>().id == blockData[i].nextId)
                    {
                        blocks[i].GetComponent<Block3D>().next = blocks[j].GetComponent<Block3D>();
                    }
                }
            }
            blockCount = blocks.Length;
        }
    }

    private class SaveMap {
        public Block3D_Data[] blockData; 
        public int playerNum;
    }

    [Serializable]
    private class Block3D_Data {
        public Vector3 position;
        public int id;
        public int nextId;
    }
}
